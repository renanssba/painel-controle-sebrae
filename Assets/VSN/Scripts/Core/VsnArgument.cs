using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class VsnArgument{

  public virtual float GetNumberValue(){
    return 0f;
  }

  public virtual string GetStringValue(){
    return "";
  }

  public virtual string GetReference(){
    return "";
  }

  public virtual bool GetBooleanValue() {
    return false;
  }

  public string GetPrintableValue() {
    if(GetType() == typeof(VsnString)) {
      return GetStringValue();
    } else if(GetType() == typeof(VsnNumber)) {
      return GetNumberValue().ToString();
    } else if (GetType() == typeof(VsnBoolean)) {
      return GetBooleanValue().ToString();
    } else if (GetType() == typeof(VsnReference)) {
      return GetReference();
    } else if (GetType() == typeof(VsnMetaReference)) {
      return "*"+GetReference();
    }
    return "null";
  }
}

