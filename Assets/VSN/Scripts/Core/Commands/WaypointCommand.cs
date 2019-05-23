using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "waypoint")]
  public class WaypointCommand : VsnCommand {

    public override void Execute() {
//      VsnController.instance.core.RegisterWaypoint(new VsnWaypoint(args[0].GetReference(), commandIndex));
    }

    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
          VsnArgType.referenceArg
        });
    }
  }
}