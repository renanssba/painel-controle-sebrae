using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class DiskSaveHandler : IVsnSaveHandler{

	private readonly string savePrefix = "VSNSAVE";

	private String GetSaveSlotPrefix(int saveSlot){
		return saveSlot + "_" + savePrefix;
	}

	void IVsnSaveHandler.Save (Dictionary<string, string> dictionary, int saveSlot, Action<bool> callback){
		bool success;
		string saveString = GetSaveSlotPrefix (saveSlot);

		Dictionary<string, string> savedDictionary = dictionary;
		string savedVariables = GenerateSavedVariables (savedDictionary);
		//savedDictionary = PrefixDictionary(dictionary, saveSlot);
		Debug.Log("Setting to playerprefs, string: " + GetSaveSlotPrefix(saveSlot) + ", value: " + savedVariables);
		PlayerPrefs.SetString(GetSaveSlotPrefix(saveSlot), savedVariables);


		/*
		Debug.Log("JSON count: " + dictionary.Count);
		finalJson = JsonUtility.ToJson(dictionary);
		Debug.Log("Saved JSON: " + finalJson);
		PlayerPrefs.SetString(saveString, finalJson);
		*/

		success = true;
		callback(success);
	}
		
	void IVsnSaveHandler.Load(Dictionary<string, string> dictionary, int saveSlot, Action<Dictionary<string,string>> callback){
		string saveString = GetSaveSlotPrefix (saveSlot);
		Dictionary<string, string> loadedDictionary = LoadSavedVariables(saveString);
		callback(loadedDictionary);
	}

	/// <summary>
	/// Generates the saved variables in format: "varname=value@varname=value@varname=value@(...)"
	/// </summary>
	/// <returns>The saved variables.</returns>
	/// <param name="dictionary">Dictionary.</param>
	string GenerateSavedVariables (Dictionary<string, string> dictionary){
		string savedVariables = "";

		foreach(KeyValuePair<string, string> entry in dictionary){
			savedVariables += entry.Key + "=" + entry.Value;
			savedVariables += "@";
		}

		return savedVariables;
	}

	Dictionary<string, string> LoadSavedVariables (string saveString){
		Dictionary<string, string> returnedDictionary = new Dictionary<string, string>();

		string loadedVariables = PlayerPrefs.GetString(saveString, "");

		string[] separatedVariablePairs = loadedVariables.Split('@');
		foreach (string variablePair in separatedVariablePairs) {			
			string[] pair = variablePair.Split ('=');
			if (pair [0] == ""){ //end of variables
				break;
			}
			if (pair.Length != 2) {
				Debug.Log("<color=red>ERROR: Invalid saved variables string: " + variablePair.ToString() + ". Expected \"varname=value\"</color>");
			}

			string variableName = pair[0];
			string variableValue = pair[1];
			returnedDictionary.Add(variableName, variableValue);
		}

		return returnedDictionary;
	}

	private Dictionary<string, string> PrefixDictionary(Dictionary<string, string> dictionary, int saveSlot){
		Dictionary<string, string> returnedDictionary = new Dictionary<string, string>();

		foreach(KeyValuePair<string, string> entry in dictionary){
			string prefixedKey = saveSlot.ToString() + "_" + entry.Key;
			returnedDictionary.Add(prefixedKey, entry.Value);
		}

		return returnedDictionary;
	}

}

