using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "end_script")]
  public class EndScriptCommand : VsnCommand {

    public override void Execute() {
      VsnController.instance.CurrentScriptReader().GotoEnd();
      if (args.Length >= 1) {
        if(VsnController.instance.PreviousScriptReaderInStack() != null){
          Debug.LogWarning("Resuming previous script in waypoint: " + args[0].GetReference());
          VsnController.instance.PreviousScriptReaderInStack().GotoWaypoint(args[0].GetReference());
        }
      }
    }

    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[0]);
      signatures.Add(new VsnArgType[] { 
        VsnArgType.referenceArg
      });
    }
  }
}