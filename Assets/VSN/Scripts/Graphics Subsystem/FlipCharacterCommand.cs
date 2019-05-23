using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "char_flip")]
  public class FlipCharacterCommand : VsnCommand {

    public override void Execute() {
      VsnUIManager.instance.FlipCharacterSprite(args[0].GetReference());
    }


    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.referenceArg
      });
    }

  }
}