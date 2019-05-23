using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command{

	[CommandAttribute(CommandString="wait")]
	public class WaitCommand : VsnCommand {

		public override void Execute (){
      VsnController.instance.WaitForSeconds(args[0].GetNumberValue());
    }

    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.numberArg
      });
    }
	}
}