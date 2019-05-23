using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "debug_var")]
  public class DebugVariableCommand : VsnCommand {

    public override void Execute() {
      //float value = args[0].GetNumberValue();

//      Debug.Log("Variable " + value + ": " + value);
    }


    public override void AddSupportedSignatures(){
      signatures.Add( new VsnArgType[]{
        VsnArgType.referenceArg
      } );

      signatures.Add( new VsnArgType[]{
        VsnArgType.numberArg
      } );
    }

  }
}