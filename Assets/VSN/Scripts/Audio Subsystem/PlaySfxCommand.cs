using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command{

	[CommandAttribute(CommandString="play_sfx")]
	public class PlaySfxCommand : VsnCommand {

		public override void Execute(){
      VsnAudioManager.instance.PlaySfx(args[0].GetStringValue());
    }

    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.stringArg
      });
    }
	}
}