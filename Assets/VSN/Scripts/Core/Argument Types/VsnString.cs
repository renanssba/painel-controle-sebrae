using System;
using UnityEngine;

public class VsnString : VsnArgument{

  protected string stringValue;

  public VsnString(string text){
		stringValue = text;
  }

  public override string GetStringValue(){
    return SpecialCodes.InterpretStrings(ReplaceSingleQuote());
  }

  public string ReplaceSingleQuote(){
    bool canReplace = false;
    char[] array = stringValue.ToCharArray();

    Debug.Log("Text: " + new string(array));

    for (int i=0; i<stringValue.Length; i++){
      switch(stringValue[i]) {
        case '<':
          canReplace = true;
          break;
        case '>':
          canReplace = false;
          break;
        case '\'':
          if(canReplace){
            array[i] = '\"';
          }
          break;
      }
    }
    return new string(array);
  }
}

