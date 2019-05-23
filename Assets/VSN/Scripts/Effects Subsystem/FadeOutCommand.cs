using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "fade_out")]
  public class FadeOutCommand : VsnCommand {

    public override void Execute() {

      if(args.Length >= 1) {
        VsnEffectManager.instance.FadeOut(args[0].GetNumberValue());
      } else {
        VsnEffectManager.instance.FadeOut(0.5f);
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