using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BasicController : MonoBehaviour {

  public static BasicController instance;
  public TextMeshProUGUI tableText;

  private void Start() {
    instance = this;
    UpdateUI();
    VsnAudioManager.instance.PlayMusic("song_intro", "song_loop");

    if(VsnSaveSystem.GetIntVariable("minigame_played")==0) {
      VsnController.instance.StartVSN("intro");
    } else {
      VsnSaveSystem.SetVariable("minigame_played", 0);
      VsnController.instance.StartVSN("return_minigame");
    }
  }

  void Update () {
		if(Input.GetKeyDown(KeyCode.F5)){
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
	}

  void UpdateUI(){
    VsnUIManager.instance.scoreText.text = VsnSaveSystem.GetIntVariable("money").ToString();
  }
}
