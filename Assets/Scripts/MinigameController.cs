using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameController : MonoBehaviour {

  public GameObject coinPrefab;
  public static MinigameController instance;
  public float waitTime = 2f;

  public int coins = 0;

  private void Awake() {
    instance = this;
  }


  void Start() {
    coins = 0;
    UpdateUI();
    StartCoroutine(SpawnCoins());
    VsnAudioManager.instance.PlayMusic("minigame_intro", "minigame_loop");
	}

  public IEnumerator SpawnCoins(){
    while(true) {
      yield return new WaitForSeconds(Random.Range(0.1f, waitTime));
      Spawn(1);
    }
  }

  public void Spawn(int count){
    for(int i=0; i<count; i++){
      GameObject obj = Instantiate(coinPrefab);
      obj.transform.position += new Vector3(Random.Range(-8f, 8f), 0f, 0f);
    }
  }

  public void GetCoin(){
    coins += 50;
    UpdateUI();
  }

  public void UpdateUI(){
    VsnUIManager.instance.scoreText.text = coins.ToString();
  }

  public void FinishMinigame(){
    Time.timeScale = 0;
    VsnSaveSystem.SetVariable("coins", coins);
    VsnSaveSystem.SetVariable("current_coins", coins);
    VsnSaveSystem.SetVariable("minigame_played", 1);
    VsnController.instance.StartVSNContent("say \"FIM DO MINIGAME!\"\nload_scene \"Main\"", "custom");
  }
}
