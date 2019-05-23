using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

  public Vector2 speed;

	void Start () {
    GetComponent<Rigidbody2D>().velocity = speed;
	}
}
