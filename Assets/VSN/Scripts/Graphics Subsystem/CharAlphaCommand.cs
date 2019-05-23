using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "char_alpha")]
  public class CharAlphaCommand : VsnCommand {

    public override void Execute() {
      VsnUIManager.instance.SetCharacterAlpha(args[0].GetReference(), args[1].GetNumberValue(), args[2].GetNumberValue());
    }

    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.referenceArg,
        VsnArgType.numberArg,
        VsnArgType.numberArg
      });
    }
  }
}