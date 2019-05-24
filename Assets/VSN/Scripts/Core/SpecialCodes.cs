using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCodes {

  public static string InterpretStrings(string initialString){
    string currentString = initialString;

    if (initialString == null) {
      return "";
    }

    if(!initialString.Contains("\\")){
      return initialString;
    }

    do {
      initialString = currentString;

      currentString = currentString.Replace("\\number_input", VsnSaveSystem.GetIntVariable("number_input").ToString());
      currentString = currentString.Replace("\\n", "\n");
      currentString = currentString.Replace("\\coins", VsnSaveSystem.GetIntVariable("coins").ToString());
      currentString = currentString.Replace("\\current_coins", VsnSaveSystem.GetIntVariable("current_coins").ToString());
    } while (currentString != initialString);

    return currentString;
  }


  public static float InterpretFloat(string keycode){
    if(!keycode.Contains("#")){
      return 0f;
    }

    return InterpretSpecialNumber(keycode);
  }

  static float InterpretSpecialNumber(string keycode){
    switch (keycode){
      case "#random100":
        return Random.Range(0, 100);
      default:
        return 0f;
    }
    return 0f;
  }
}
