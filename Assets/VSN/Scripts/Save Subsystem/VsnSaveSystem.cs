using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VsnSaveSystem {

  static Dictionary<string, string> savedDataDictionary;

  static readonly string varFloatPrefix = "VARNUMBER";
  static readonly string varStringPrefix = "VARSTRING";

  private static int saveSlot;
  private static IVsnSaveHandler saveHandler;

  #region getters/setters

  /// <summary>
  /// Gets or sets the current save slot. Starting slot is 1.
  /// Use 1 or more for actual save slots.
  /// </summary>
  /// <value>The save file.</value>
  public static int SaveSlot {
    get {
      return saveSlot;
    }
    set {
      if(value >= 1) {
        saveSlot = value;	
      }
    }
  }

  public static IVsnSaveHandler SaveHandler {
    get;
    set;
  }

  #endregion

  static VsnSaveSystem() {
    SaveSlot = 1;
    SaveHandler = new DiskSaveHandler();

    savedDataDictionary = new Dictionary<string, string>();
  }

  #region Prefixes

  static string GetVariableFloatPrefix(string key) {
    return varFloatPrefix + "_" + key;
  }

  static string GetVariableStringPrefix(string key) {
    return varStringPrefix + "_" + key;
  }

  #endregion

  #region Variables (sets, adds, gets)

  public static void SetVariable(string key, int value) {
    SetVariable(key, (float)value);
  }

  public static void SetVariable(string key, float value) {
    Debug.Log("Variable " + key + " saved with float value " + value);
    string savedKey = GetVariableFloatPrefix(key);

    if(savedDataDictionary.ContainsKey(savedKey)) {
      savedDataDictionary[savedKey] = value.ToString();
    } else {
      savedDataDictionary.Add(savedKey, value.ToString());
    }
    Save(0);
  }

  public static void SetVariable(string key, bool value) {
    Debug.Log("Variable " + key + " saved with bool value " + value);
    string savedKey = GetVariableFloatPrefix(key);

    if (savedDataDictionary.ContainsKey(savedKey)) {
      savedDataDictionary[savedKey] = value.ToString();
    } else {
      savedDataDictionary.Add(savedKey, value.ToString());
    }
    Save(0);
  }

  public static void SetVariable(string key, string value) {
    Debug.Log("Variable " + key + " saved with string value " + value);
    string savedKey = GetVariableStringPrefix(key);

    if(savedDataDictionary.ContainsKey(savedKey)) {
      savedDataDictionary[savedKey] = value;
    } else {
      savedDataDictionary.Add(savedKey, value);
    }
    Save(0);
  }


  public static void AddVariable(string key, float amount) {
    string savedKey = GetVariableFloatPrefix(key);

    if(savedDataDictionary.ContainsKey(savedKey)) {			
      float currentValue;
      if(float.TryParse(savedDataDictionary[savedKey], out currentValue)) {
        savedDataDictionary[savedKey] = (currentValue + amount).ToString();
      }

    } else {
      savedDataDictionary.Add(savedKey, amount.ToString());
    }
    Save(0);
  }

  public static int GetIntVariable(string key, int defaultValue = 0) {
    return (int)GetFloatVariable(key, (float)defaultValue);
  }

  public static float GetFloatVariable(string key, float defaultValue = 0f) {
    string savedKey = GetVariableFloatPrefix(key);

    if(savedDataDictionary.ContainsKey(savedKey)) {
      float currentValue;
      if(float.TryParse(savedDataDictionary[savedKey], out currentValue)) {	
        return currentValue;
      }
    }

    return defaultValue;
  }

  public static string GetStringVariable(string key, string defaultValue = "") {
    string savedKey = GetVariableStringPrefix(key);

    if(savedDataDictionary.ContainsKey(savedKey)) {
      return savedDataDictionary[savedKey];
    }

    return defaultValue;
  }

  #endregion

  #region save/load

  public static void Save(int saveSlot) {		
    SaveHandler.Save(savedDataDictionary, saveSlot, (bool success) => {
      if(success) {
        Debug.Log("VSN SAVE success");
      }
    });
  }

  public static void Load(int saveSlot) {
    SaveHandler.Load(savedDataDictionary, saveSlot, (Dictionary<string,string> dictionary) => {
      if(dictionary != null) {
        savedDataDictionary = dictionary;
        Debug.Log("VSN LOAD success");
      }
    });
  }

  #endregion

}
