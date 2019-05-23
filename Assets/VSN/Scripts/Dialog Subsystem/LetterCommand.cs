using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "letter")]
  public class LetterCommand : VsnCommand {

    public override void Execute() {
      VsnController.instance.state = ExecutionState.WAITINGTOUCH;

      if(args.Length == 2) {
        VsnUIManager.instance.OpenLetterPanel(args[0].GetStringValue(), args[1].GetStringValue());
      } else {
        VsnUIManager.instance.OpenLetterPanel();
      }
    }


    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.stringArg,
        VsnArgType.stringArg
      });

      signatures.Add(new VsnArgType[0]);
    }
  }
}