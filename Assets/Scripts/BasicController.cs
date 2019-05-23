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
    VsnAudioManager.instance.PlayMusic("song_intro", "song_loop");
  }

  void Update () {
		if(Input.GetKeyDown(KeyCode.F5)){
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
	}
}
