using System;
using UnityEngine;

public class VsnMetaReference : VsnArgument {

  protected string metareferenceValue;

  public VsnMetaReference(string metareferenceName) {
    metareferenceValue = metareferenceName;
  }

  public override string GetReference() {
    Debug.LogWarning("Meta value: " + metareferenceValue);
    Debug.LogWarning("Return from meta-reference: " + VsnSaveSystem.GetStringVariable(metareferenceValue));
    return VsnSaveSystem.GetStringVariable(metareferenceValue);
  }
}

