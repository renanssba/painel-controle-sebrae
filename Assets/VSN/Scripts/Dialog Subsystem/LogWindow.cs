using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogWindow : MonoBehaviour {
  
  public GameObject logWindow;
  public TextMeshProUGUI logText;
  public ScrollRect logScrollRect;

  public string logString;


  public void Awake(){
//    ResetLog();
  }


  public void AddToLog(string text){
    AddToLog(null, text);
  }


  public void AddToLog(string speakerName, string text){
    if(logString != "") {
      logString += "\n\n";
    }

    if(speakerName != null && speakerName.CompareTo("")!=0){
      logString += "<color=#E18F9D>" + speakerName + "</color>\n";
    }
    logString += text;
    UpdateText();
  }

  public void ResetLog(){
    Debug.LogWarning("Clearing log");
    logString = "";
    UpdateText();    
  }

  void UpdateText(){
    logText.text = logString;
  }


  public void OpenLogWindow(){
    logWindow.SetActive(true);
    logScrollRect.verticalNormalizedPosition = 0f;
  }

  public void CloseLogWindow(){
    logWindow.SetActive(false);
  }
}
