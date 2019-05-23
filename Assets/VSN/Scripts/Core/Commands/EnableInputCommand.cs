using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "enable_input")]
  public class EnableInputCommand : VsnCommand {

    public override void Execute() {
      if(args[0].GetStringValue() == "true") {
        VsnController.instance.BlockExternalInput(false);
      } else if(args[0].GetStringValue() == "false") {
        VsnController.instance.BlockExternalInput(true);
      } else {
        Debug.LogError("Invalid parameter");
      }
    }

    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.stringArg
      });
    }
  }
}