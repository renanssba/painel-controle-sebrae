using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Command;
using TMPro;
using TMPro.Examples;
using DG.Tweening;

public class VsnUIManager : MonoBehaviour {

  public static VsnUIManager instance;

  public GameObject vsnMessagePanel;
  public GameObject clickMessageIcon;
  public TextMeshProUGUI vsnMessageText;
  public TextMeshProUGUI vsnMessageTitle;
  public Image vsnMessageTitlePanel;
  public Button screenButton;
  public RectTransform charactersPanel;
  public Image backgroundImage;
  public GameObject choicesPanel;
  public Button[] choicesButtons;
  public TextMeshProUGUI[] choicesTexts;

  public GameObject letterPanel;
  public TextMeshProUGUI letterTitleText;
  public TextMeshProUGUI letterText;
  public ScrollRect letterScrollRect;

  public GameObject textInputPanel;
  public TextMeshProUGUI textInputPanelDescriptionText;
  public TMP_InputField textInputField;

  public Button skipButton;
  public string skipButtonWaypoint;

  public GameObject vsnCharacterPrefab;

  public bool isTextAppearing;

  public int charsToShowPerSecond = 50;

  public List<VsnCharacter> characters;

  public TextMeshProUGUI scoreText;

  void Awake() {
    if(instance == null) {
      instance = this;
    }

    screenButton.onClick.AddListener(OnScreenButtonClick);
    characters = new List<VsnCharacter>();
  }


  public static void SelectUiElement(GameObject toSelect) {
    EventSystem.current.SetSelectedGameObject(toSelect);
  }


  public void ShowDialogPanel(bool value) {
    vsnMessagePanel.SetActive(value);
  }

  public void SetText(string msg) {
    ShowClickMessageIcon(false);
    if(!string.IsNullOrEmpty(vsnMessageTitle.text)) {
      vsnMessageText.text = "\""+msg+ "\"";
    } else{
      vsnMessageText.text = msg;
    }    
    vsnMessageText.GetComponent<VsnConsoleSimulator>().StartShowingCharacters();
  }

  public void SetTextAuto() {
    vsnMessageText.GetComponent<VsnConsoleSimulator>().SetAutoPassText(true);
  }

  public void ShowClickMessageIcon(bool value){
    clickMessageIcon.SetActive(value);
  }

  public void SetTextTitle(string messageTitle) {
    vsnMessageTitle.text = messageTitle;
    if(string.IsNullOrEmpty(messageTitle)) {
      vsnMessageTitlePanel.gameObject.SetActive(false);
    } else {
      vsnMessageTitlePanel.gameObject.SetActive(true);
    }
  }

  public void OnScreenButtonClick() {
    if(isTextAppearing) {
      isTextAppearing = false;
      vsnMessageText.GetComponent<VsnConsoleSimulator>().FinishShowingCharacters();
    } else if(VsnController.instance.state == ExecutionState.WAITINGTOUCH) {
      //VsnAudioManager.instance.PlaySfx("ui_dialogue_advance");
      VsnController.instance.state = ExecutionState.PLAYING;
      ShowClickMessageIcon(false);
      ShowDialogPanel(false);
    }
  }

  private void AddChoiceButtonListener(Button button, string label) {
    button.onClick.RemoveAllListeners();
    button.onClick.AddListener(() => {
      VsnCommand command = new GotoCommand();
      List<VsnArgument> arguments = new List<VsnArgument>();
      arguments.Add(new VsnReference(label));

      VsnAudioManager.instance.PlaySfx("ui_confirm");

      command.InjectArguments(arguments);
      ShowChoicesPanel(false, 0);
      ShowDialogPanel(false);
      command.Execute();
      VsnController.instance.state = ExecutionState.PLAYING;
    });
  }

  public void ShowChoicesPanel(bool enable, int numberOfChoices) {
    choicesPanel.SetActive(enable);

    if(enable) {
      for(int i = 0; i < choicesButtons.Length; i++) {
        bool willSetActive = (i < numberOfChoices);
        choicesButtons[i].gameObject.SetActive(willSetActive);
      }
    }
  }

  public void SetChoicesTexts(string[] choices) {
    for(int i = 0; i < choices.Length; i++) {
      choicesTexts[i].text = choices[i];
    }
  }

  public void SetChoicesLabels(string[] labels) {
    for(int i = 0; i < labels.Length; i++) {
      AddChoiceButtonListener(choicesButtons[i], labels[i]);
    }
  }

  public void OpenLetterPanel(string letterTitle, string letterContent){
    letterTitleText.text = letterTitle;
    letterText.text = letterContent;
    OpenLetterPanel();
  }

  public void OpenLetterPanel(){
    letterScrollRect.verticalNormalizedPosition = 1f;
    letterPanel.SetActive(true);
  }

  public void CloseLetterPanel(){
    letterPanel.SetActive(false);
    VsnController.instance.state = ExecutionState.PLAYING;
  }

  public void CreateNewCharacter(Sprite characterSprite, string characterFilename, string characterLabel) {
    GameObject vsnCharacterObject = Instantiate(vsnCharacterPrefab, charactersPanel.transform) as GameObject;
    vsnCharacterObject.transform.localScale = Vector3.one;
    VsnCharacter vsnCharacter = vsnCharacterObject.GetComponent<VsnCharacter>();

    Vector2 newPosition = Vector2.zero;
    vsnCharacter.GetComponent<RectTransform>().anchoredPosition = newPosition;

    vsnCharacter.GetComponent<Image>().sprite = characterSprite;
    vsnCharacter.label = characterLabel;
    vsnCharacter.characterFilename = characterFilename;

    characters.Add(vsnCharacter);
  }

  public void MoveCharacterX(string characterLabel, float position, float duration) {
    float screenPosition = GetCharacterScreenPositionX(position);
    VsnCharacter character = FindCharacterByLabel(characterLabel);

    Debug.LogWarning("Original pos: "+position+", final pos: " + screenPosition);

    if(character != null) {
      Vector2 newPosition = new Vector2(screenPosition, character.GetComponent<RectTransform>().anchoredPosition.y);
      if(duration != 0) {
        character.GetComponent<RectTransform>().DOAnchorPos(newPosition, duration);
      } else {
        character.GetComponent<RectTransform>().anchoredPosition = newPosition;
      }
    }
  }

  public void MoveCharacterY(string characterLabel, float position, float duration) {
    float screenPosition = GetCharacterScreenPositionY(position);
    VsnCharacter character = FindCharacterByLabel(characterLabel);

    if(character != null) {
      Vector2 newPosition = new Vector2(character.GetComponent<RectTransform>().anchoredPosition.x, screenPosition);
      if(duration != 0) {
        character.GetComponent<RectTransform>().DOAnchorPos(newPosition, duration);
      } else {
        character.GetComponent<RectTransform>().anchoredPosition = newPosition;
      }
    }

  }

  public void SetCharacterAlpha(string characterLabel, float alphaValue, float duration) {
    VsnCharacter character = FindCharacterByLabel(characterLabel);

    if(character != null) {
      Image characterImage = character.GetComponent<Image>();
      if(duration != 0) {
        characterImage.DOFade(alphaValue, duration);
      } else {
        characterImage.color = new Color(characterImage.color.r,
                                         characterImage.color.g,
                                         characterImage.color.b, alphaValue);
      }
    }
  }

  private float GetCharacterScreenPositionX(float normalizedPositionX) {
    float zeroPoint = -charactersPanel.rect.width/2f;
    float onePoint = charactersPanel.rect.width/2f;
    float totalSize = onePoint - zeroPoint;

    float finalPositionX = zeroPoint + normalizedPositionX * totalSize;
    return finalPositionX;
  }

  private float GetCharacterScreenPositionY(float normalizedPositionY) {
    int maxPoint = 500;
    int minPoint = 200;
    int totalPoints = Mathf.Abs(maxPoint) + Mathf.Abs(minPoint);

    if(normalizedPositionY < 0f)
      return minPoint;
    else if(normalizedPositionY > 1f)
      return maxPoint;

    float finalPositionY = normalizedPositionY * totalPoints;
    Debug.Log("Final Y: " + finalPositionY);
    return finalPositionY;
  }

  private VsnCharacter FindCharacterByLabel(string characterLabel) {
    foreach(VsnCharacter character in characters) {
      if(character.label == characterLabel) {
        return character;
      }
    }
    return null;
  }

  public void FlipCharacterSprite(string characterLabel) {
    VsnCharacter character = FindCharacterByLabel(characterLabel);

    if(character == null){
      Debug.LogError("Error flipping character " + characterLabel + ". Character not found with this label.");
      return;
    }

    Vector3 localScale = character.transform.localScale;
    character.transform.localScale = new Vector3(localScale.x * -1f, localScale.y, localScale.z);
  }


  public void ResetAllCharacters() {
    foreach(VsnCharacter character in characters) {
      Destroy(character.gameObject);
    }
    characters.Clear();
  }

  public void SetBackground(Sprite backgroundSprite) {
    if(backgroundSprite != null) {
      backgroundImage.sprite = backgroundSprite;
      backgroundImage.gameObject.SetActive(true);
    } else {
      ResetBackground();
    }
  }

  public void ResetBackground() {
    backgroundImage.gameObject.SetActive(false);
  }


  public void SetSkipButtonWaypoint(string waypoint){
    skipButtonWaypoint = waypoint;
    AddChoiceButtonListener(skipButton, waypoint);
  }

  public void ShowSkipButton(bool show){
    skipButton.gameObject.SetActive(show);
  }

  public void SetTextInputDescription(string msg){
    textInputPanelDescriptionText.text = msg;
  }

  public void SetTextInputCharacterLimit(int limit){
    textInputField.characterLimit = limit;
  }

  public void ShowTextInput(bool show){
    textInputPanel.SetActive(show);
    if(show == true){
      VsnUIManager.SelectUiElement(null);
      textInputField.text = "";
      VsnUIManager.SelectUiElement(textInputField.gameObject);
    }
  }

  public void OnTextInputConfirm() {
    if(string.IsNullOrEmpty(textInputField.text)){
//      SoundManager.PlayForbiddenSound();
      return;
    }

    VsnController.instance.state = ExecutionState.PLAYING;
    if(textInputField.text != ""){
      VsnSaveSystem.SetVariable("text_input", textInputField.text);
      VsnSaveSystem.SetVariable("number_input", int.Parse(textInputField.text));
    }
    ShowTextInput(false);
  }
}

