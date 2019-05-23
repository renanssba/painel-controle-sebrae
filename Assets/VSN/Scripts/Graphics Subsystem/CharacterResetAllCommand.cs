using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

	[CommandAttribute(CommandString = "char_reset_all")]
	public class CharacterResetAllCommand : VsnCommand {
    
		public override void Execute(){
			VsnUIManager.instance.ResetAllCharacters();
    }

    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[0]);
    }
	}
}