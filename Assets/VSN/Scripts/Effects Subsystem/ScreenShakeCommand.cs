using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command{

	[CommandAttribute(CommandString="shake")]
	public class ScreenShakeCommand : VsnCommand {

		public override void Execute (){

      switch(args.Length){
        case 0:
          VsnEffectManager.instance.ScreenShake(0.5f, 1f);
          break;
        case 1:
          VsnEffectManager.instance.ScreenShake(args[0].GetNumberValue(), 1f);
          break;
        case 2:
          VsnEffectManager.instance.ScreenShake(args[0].GetNumberValue(), args[1].GetNumberValue());
          break;
      }
		}


    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[0]);

      signatures.Add(new VsnArgType[] {
        VsnArgType.numberArg
      });

      signatures.Add(new VsnArgType[] {
        VsnArgType.numberArg,
        VsnArgType.numberArg
      });
    }
	}
}