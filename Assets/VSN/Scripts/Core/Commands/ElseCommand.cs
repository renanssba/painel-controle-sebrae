using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command{

	[CommandAttribute(CommandString="else")]
	public class ElseCommand : VsnCommand {

		public override void Execute(){
//      Debug.LogWarning("Executing else. does nothing");
      VsnController.instance.CurrentScriptReader().GotoNextElseOrEndif();
    }

    public override void AddSupportedSignatures(){
      signatures.Add(new VsnArgType[0]);
    }
	}
}