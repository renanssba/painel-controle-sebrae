using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VsnArgType{
  numberArg,
  stringArg,
  referenceArg,
  booleanArg,
  operatorArg
};

[System.Serializable]
public abstract class VsnCommand{

  protected VsnArgument[] args;
  protected List<VsnArgType[]> signatures;

	public int commandIndex = -1; //VsnCore sets this
  public int fileLineId;

  public abstract void Execute();
  public abstract void AddSupportedSignatures();


  public VsnCommand(){
    signatures = new List<VsnArgType[]>();
    AddSupportedSignatures();
  }

  public void InjectArguments(List<VsnArgument> arguments){
    args = arguments.ToArray();
  }

  public virtual bool CheckSyntax(){
    for(int i=0 ; i<signatures.Count; i++){
      if(IsValidSignature(signatures[i])){
        return true;
      }
    }
    return false;
  }


  public bool IsValidSignature(VsnArgType[] signature){
    if(args.Length != signature.Length){
      return false;
    }

    for(int j=0 ; j<args.Length; j++){
      if(ArgumentMatchesType(args[j], signature[j]) == false){
        return false;
      }
    }
    return true;
  }


  public bool ArgumentMatchesType(VsnArgument arg, VsnArgType type){
    switch(type){
      case VsnArgType.numberArg:
        return (arg.GetType() == typeof(VsnNumber) ||
                arg.GetType() == typeof(VsnReference));

      case VsnArgType.stringArg:
        return (arg.GetType() == typeof(VsnString) ||
                arg.GetType() == typeof(VsnReference));

      case VsnArgType.booleanArg:
        return (arg.GetType() == typeof(VsnBoolean) ||
                arg.GetType() == typeof(VsnReference));

      case VsnArgType.operatorArg:
        return (arg.GetType() == typeof(VsnOperator));

      case VsnArgType.referenceArg:
        return (arg.GetType() == typeof(VsnReference) ||
                arg.GetType() == typeof(VsnMetaReference));
    }
    return false;
  }
}
