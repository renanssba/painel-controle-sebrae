using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using System;
using System.Reflection;
using Command;

public enum ExecutionState {
  STARTING,
  PLAYING,
  WAITING,
  WAITINGTOUCH,
  WAITINGCHOICE,
  WAITINGTEXTINPUT,
  WAITINGCUSTOMINPUT,
  PAUSED,
  STOPPED,
  NumberOfExecutionStates
}

public delegate bool CustomEventDelegate();


public class VsnController : MonoBehaviour {

  public static VsnController instance;
  public List<Type> possibleCommandTypes;
  public ExecutionState state;
  public GameObject inputBlocker;
  public GameObject vsnCanvas;

  public bool clearGraphicsWhenStop = true;

  public List<VsnScriptReader> scriptsStack;
  public List<VsnScriptReader> nextScripts;

  void Awake() {
    if(instance == null) {
      instance = this;
      possibleCommandTypes = GetClasses("Command");
      scriptsStack = new List<VsnScriptReader>();
      nextScripts = new List<VsnScriptReader>();
    }
  }


  /// <summary>
  /// Starts VSN with a given script path, starting from Resources root.
  /// </summary>
  /// <param name="scriptPath">Script path from Resources root (e.g \"VSN Scripts/myscript.txt\"</param>
  public void StartVSN(string scriptPath, VsnArgument[] args = null) {
    scriptPath = scriptPath;
    TextAsset textContent = Resources.Load<TextAsset>(scriptPath);
    if(textContent == null){
      Debug.LogWarning("Error loading VSN Script: " + scriptPath + ". Please verify the provided path.");
      return;
    }

    if(state == ExecutionState.STOPPED) {
      StartVSNContent(textContent.text, scriptPath, args);
    } else {
      nextScripts.Add(new VsnScriptReader());
      nextScripts[nextScripts.Count-1].LoadScriptContent(textContent.text, scriptPath, args);
    }
  }

  public void StartVSNContent(string content, string scriptPath, VsnArgument[] args = null){
    if(state == ExecutionState.STOPPED) {
      CleanVsnElements();

      scriptsStack.Add(new VsnScriptReader());
      CurrentScriptReader().LoadScriptContent(content, scriptPath, args);
      ResumeVSN();
    } else {
      nextScripts.Add(new VsnScriptReader());
      nextScripts[nextScripts.Count-1].LoadScriptContent(content, scriptPath, args);
    }
  }

  void CleanVsnElements(){
    vsnCanvas.SetActive(true);
    BlockExternalInput(true);
    VsnUIManager.instance.SetTextTitle("");
    VsnUIManager.instance.ShowSkipButton(false);
    VsnUIManager.SelectUiElement(null);
  }

  public void GotoVSNScript(string scriptPath, VsnArgument[] args){
    scriptPath = scriptPath;
    TextAsset textAsset = Resources.Load<TextAsset>(scriptPath);
    if(textAsset == null){
      Debug.LogWarning("Error loading VSN Script: " + scriptPath + ". Please verify the provided path.");
      return;
    }
//    CurrentScriptReader().currentCommandIndex++;
    PauseVSN();

    if (CurrentScriptReader().currentCommandIndex >= CurrentScriptReader().vsnCommands.Count) {
      Debug.LogWarning("removing top script reader: " + CurrentScriptReader().loadedScriptName);
      scriptsStack.RemoveAt(scriptsStack.Count - 1);
    }

    scriptsStack.Add(new VsnScriptReader());
    CurrentScriptReader().LoadScriptContent(textAsset.text, scriptPath, args);
    ResumeVSN();
  }

  public VsnScriptReader CurrentScriptReader(){
    return scriptsStack[scriptsStack.Count-1];
  }

  public VsnScriptReader PreviousScriptReaderInStack() {
    if(scriptsStack.Count > 2){
      return scriptsStack[scriptsStack.Count - 2];
    }
    return null;
  }


  IEnumerator ExecuteScript() {
    VsnScriptReader reader = CurrentScriptReader();
    while(!reader.HasFinished()){
      reader.ExecuteCurrentCommand();
      while(state != ExecutionState.PLAYING) {
        yield return null;
      }
    }
    FinishScript();
  }

  public void ResumeVSN() {
    state = ExecutionState.PLAYING;
    CurrentScriptReader().SetArgs();
    StartCoroutine(ExecuteScript());
  }

  public void PauseVSN() {
    state = ExecutionState.PAUSED;
    StopAllCoroutines();
  }

  public void WaitForSeconds(float duration) {
    state = ExecutionState.WAITING;
    StartCoroutine(Wait(duration));
  }

  private IEnumerator Wait(float waitTime) {
    yield return new WaitForSecondsRealtime(waitTime);
    state = ExecutionState.PLAYING;
  }

  public void WaitForCustomInput(CustomEventDelegate customEventDelegate) {
    WaitForCustomInput();
    StartCoroutine(WaitingCustomInput(customEventDelegate));
  }

  public void WaitForCustomInput() {
    state = ExecutionState.WAITINGCUSTOMINPUT;
  }

  public IEnumerator WaitingCustomInput(CustomEventDelegate customEventDelegate){
    while(true){
      if(customEventDelegate()){
        break;
      }
      yield return null;
    }
    GotCustomInput();
  }

  public void GotCustomInput(){
    if(state == ExecutionState.WAITINGCUSTOMINPUT){
      state = ExecutionState.PLAYING;
    }
  }

  public void GotPersonInput(){
    if (state == ExecutionState.WAITINGCUSTOMINPUT) {
      state = ExecutionState.PLAYING;
    }
  }

  public void GotItemInput() {
    // TODO: BETTER IMPLEMENT THIS
    if (state == ExecutionState.WAITINGCUSTOMINPUT) {
      state = ExecutionState.PLAYING;
    }
  }

  public void FinishScript(){
    //Debug.Log("finishing script");
    scriptsStack.RemoveAt(scriptsStack.Count-1);
    if(scriptsStack.Count > 0) {
      //Debug.Log("resuming script");
      ResumeVSN();
    } else {
      FinishVSN();
    }

    if(nextScripts.Count > 0) {
      scriptsStack.Add(nextScripts[0]);
      nextScripts.RemoveAt(0);
      CleanVsnElements();
      ResumeVSN();
    }
  }

  void FinishVSN(){
    Debug.LogWarning("Finishing VSN execution!");

    state = ExecutionState.STOPPED;
    if(clearGraphicsWhenStop){
      VsnUIManager.instance.ResetBackground();
      VsnUIManager.instance.ResetAllCharacters();
      VsnUIManager.instance.ShowSkipButton(false);
    }
    BlockExternalInput(false);
  }

  public void BlockExternalInput(bool value){
    if(inputBlocker != null){
      inputBlocker.SetActive(value);
    }
  }


  private List<Type> GetClasses(string nameSpace) {
    Assembly asm = Assembly.GetExecutingAssembly();

    List<Type> typeList = new List<Type>();

    foreach(Type type in asm.GetTypes()) {
      if(type.Namespace == nameSpace)
        typeList.Add(type);
    }

    return typeList;
  }


  public void Update(){
    if(Input.GetKeyDown(KeyCode.KeypadEnter) ||
       Input.GetKeyDown(KeyCode.Return) ||
       Input.GetKeyDown(KeyCode.Space)){
      switch(state){
        case ExecutionState.WAITINGTEXTINPUT:
          if(Input.GetKeyDown(KeyCode.Space)){
            break;
          }
          VsnUIManager.instance.OnTextInputConfirm();
          break;
        case ExecutionState.WAITINGTOUCH:
          VsnUIManager.instance.OnScreenButtonClick();
          break;
      }
    }
  }
}
