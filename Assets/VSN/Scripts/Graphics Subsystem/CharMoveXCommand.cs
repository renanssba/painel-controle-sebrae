using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command{

	[CommandAttribute(CommandString = "char_move_x")]
	public class CharMoveXCommand : VsnCommand {
    
		public override void Execute (){
      string characterLabel;
      float characterPositionX;
      float duration = 0;

      characterLabel = args[0].GetReference();
      characterPositionX = args[1].GetNumberValue();
      if(args.Length >= 3) {
        duration = args[2].GetNumberValue();
      }

			VsnUIManager.instance.MoveCharacterX(characterLabel, characterPositionX, duration);
    }


    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.referenceArg,
        VsnArgType.numberArg
      });

      signatures.Add(new VsnArgType[] {
        VsnArgType.referenceArg,
        VsnArgType.numberArg,
        VsnArgType.numberArg
      });
    }
	}
}