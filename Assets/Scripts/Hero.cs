using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

  public float speed;
  public float tolerance;
  public Camera current;

	void Start() {
    //current = Camera.current;
  }
	
	void Update () {
    float distx = Input.mousePosition.x - current.WorldToScreenPoint(transform.position).x;

    if(Mathf.Abs(distx) <= tolerance){
      return;
    }

    if (distx > 0) {
      transform.position += new Vector3(Time.deltaTime * speed, 0f, 0f);
    }else if(distx < 0) {
      transform.position -= new Vector3(Time.deltaTime * speed, 0f, 0f);
    }
	}

  public void OnTriggerEnter2D(Collider2D collision) {
    Debug.LogWarning("Pegou moeda");
    Destroy(collision.gameObject);
    VsnAudioManager.instance.PlaySfx("ui_confirm");
    MinigameController.instance.GetCoin();
  }
}
