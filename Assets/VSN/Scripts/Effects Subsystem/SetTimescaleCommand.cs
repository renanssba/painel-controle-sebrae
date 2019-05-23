using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "set_timescale")]
  public class SetTimescaleCommand : VsnCommand {

    public override void Execute() {
      Time.timeScale = args[0].GetNumberValue();
    }


    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.numberArg
      });
    }
  }
}