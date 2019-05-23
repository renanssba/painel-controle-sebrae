using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command{

	[CommandAttribute(CommandString="flash")]
	public class FlashScreenCommand : VsnCommand {

		public override void Execute (){
      if(args.Length >= 1) {
        VsnEffectManager.instance.FlashScreen(args[0].GetNumberValue());
      } else {
        VsnEffectManager.instance.FlashScreen(0.2f);
      }
		}


    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[0]);

      signatures.Add(new VsnArgType[] {
        VsnArgType.numberArg
      });
    }
	}
}