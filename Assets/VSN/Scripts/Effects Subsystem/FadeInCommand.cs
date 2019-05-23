using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "fade_in")]
  public class FadeInCommand : VsnCommand {

    public override void Execute() {

      if(args.Length >= 1) {
        VsnEffectManager.instance.FadeIn(args[0].GetNumberValue());
      } else {
        VsnEffectManager.instance.FadeIn(0.5f);
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