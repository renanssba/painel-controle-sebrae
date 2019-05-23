using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "if")]
  public class IfCommand : VsnCommand {

    public override void Execute() {
			bool comparisonResult = ((VsnOperator)args[1]).EvaluateComparison(args[0], args[2]);
      if(comparisonResult == false) {
        VsnController.instance.CurrentScriptReader().GotoNextElseOrEndif();
      }
    }


    public override void AddSupportedSignatures(){
      signatures.Add( new VsnArgType[]{
        VsnArgType.numberArg,
        VsnArgType.operatorArg,
        VsnArgType.numberArg
      } );

      signatures.Add( new VsnArgType[]{
        VsnArgType.stringArg,
        VsnArgType.operatorArg,
        VsnArgType.stringArg
      } );
    }
  }
}