using System;
using UnityEngine;

public class VsnCharacter : MonoBehaviour{
	
	public string characterFilename;
	public string label;

	public void SetData(string characterFilename, string label){
		this.characterFilename = characterFilename;
    this.label = label;
	}
}


