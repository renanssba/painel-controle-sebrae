using UnityEngine;
using System.Collections;
using TMPro;

public class VsnConsoleSimulator : MonoBehaviour {
  public TMP_Text TmpText;

  int totalCharacters;
  public bool autopass = false;

  Coroutine showLettersCoroutine = null;

  void Awake() {
    TmpText = gameObject.GetComponent<TMP_Text>();
  }


  public void SetAutoPassText(bool value){
    autopass = value;
  }
  public void StartShowingCharacters(){
    showLettersCoroutine = StartCoroutine(RevealCharacters());
  }

  public void FinishShowingCharacters(){
    VsnUIManager.instance.ShowClickMessageIcon(true);
    VsnUIManager.instance.isTextAppearing = false;

    TmpText.maxVisibleCharacters = totalCharacters;
    TmpText.ForceMeshUpdate();
    if(showLettersCoroutine != null){
      StopCoroutine(showLettersCoroutine);
    }
    showLettersCoroutine = null;
    if(autopass) {
      VsnUIManager.instance.OnScreenButtonClick();
      autopass = false;
    }
  }


  public IEnumerator RevealCharacters() {
    VsnUIManager.instance.isTextAppearing = true;
    TMP_TextInfo textInfo = TmpText.textInfo;
    int numberOfCharsToShow;
    float elapsedTime = 0f;
    float lastPlayedSfx = 0f;
    TmpText.ForceMeshUpdate();

    numberOfCharsToShow = 0;
    totalCharacters = textInfo.characterCount;

    while(numberOfCharsToShow < totalCharacters) {
      TmpText.maxVisibleCharacters = numberOfCharsToShow;

      elapsedTime += Time.unscaledDeltaTime;
      numberOfCharsToShow = (int)(elapsedTime * VsnUIManager.instance.charsToShowPerSecond);
      if(elapsedTime - lastPlayedSfx > VsnAudioManager.instance.dialogSfxTime){
        lastPlayedSfx = elapsedTime;
        VsnAudioManager.instance.PlayDialogSfx();
      }
      yield return null;
    }
    FinishShowingCharacters();
  }
}
