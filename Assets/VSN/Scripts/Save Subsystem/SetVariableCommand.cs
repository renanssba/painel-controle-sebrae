using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "set_var")]
  public class SetVariableCommand : VsnCommand {

    public override void Execute() {
      float fvalue = args[1].GetNumberValue();
      string svalue = args[1].GetStringValue();
      Debug.Log("SET VAR FLOAT: "+fvalue+  ", STRING: " + svalue);

      if(args[1].GetType() == typeof(VsnString) ||
        (args[1].GetType() == typeof(VsnReference) && args[1].GetStringValue() != "")){
        VsnSaveSystem.SetVariable(args[0].GetReference(), args[1].GetStringValue());
        return;
      } else if(args[1].GetType() == typeof(VsnNumber) ||
        (args[1].GetType() == typeof(VsnReference) && args[1].GetReference()[0] == '#')) {
        VsnSaveSystem.SetVariable(args[0].GetReference(), args[1].GetNumberValue());
        return;
      }

      Debug.LogWarning(args[0].GetReference() + " var value: " + args[1].GetPrintableValue());
    }

    public override void AddSupportedSignatures(){
      signatures.Add( new VsnArgType[]{
        VsnArgType.referenceArg,
        VsnArgType.numberArg
      } );

      signatures.Add( new VsnArgType[]{
        VsnArgType.referenceArg,
        VsnArgType.stringArg
      } );
    }
  }
}