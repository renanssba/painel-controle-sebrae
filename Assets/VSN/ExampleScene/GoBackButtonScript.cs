using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackButtonScript : MonoBehaviour {

	public void GoBack(){
		SceneManager.LoadScene("ExampleVSN");
	}
}
