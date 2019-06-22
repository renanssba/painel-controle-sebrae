using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple movement script for the coins to move in a fixed direction.
/// </summary>
public class Move : MonoBehaviour {

  public Vector2 speed;

	void Start () {
    GetComponent<Rigidbody2D>().velocity = speed;
	}
}
