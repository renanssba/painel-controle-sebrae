using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command{

	[CommandAttribute(CommandString="char_move_y")]
	public class CharMoveYCommand : VsnCommand {

    public override void Execute (){
      string characterLabel;
      float characterPositionY;
      float duration = 0;

      characterLabel = args[0].GetReference();
      characterPositionY = args[1].GetNumberValue();
      if(args.Length >= 3) {
        duration = args[2].GetNumberValue();
      }

      VsnUIManager.instance.MoveCharacterY(characterLabel, characterPositionY, duration);
    }


    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.stringArg,
        VsnArgType.numberArg
      });

      signatures.Add(new VsnArgType[] {
        VsnArgType.stringArg,
        VsnArgType.numberArg,
        VsnArgType.numberArg
      });
    }
	}
}