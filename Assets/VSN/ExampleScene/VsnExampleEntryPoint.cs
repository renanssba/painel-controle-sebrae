using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Example entry point for the new VSN system. Just call VsnController.instance.StartVSN with the script path.
/// </summary>
public class VsnExampleEntryPoint : MonoBehaviour {

  public string scriptToPlay;


	void Start () {
		// Script is in the Resources folder. (e.g "VSN Scripts/example3" loads from "VSN/Resources/VSN Scripts/example3").
		// "example1": basic Say, goto, waypoint
		// "example2": say with rich tags, choices
		// "example3": characters, alpha, move, say with text
		// "example4": example of transition with movex and wait with a character

		VsnSaveSystem.Load(0);
    VsnController.instance.StartVSN(scriptToPlay);
	}

}
