using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "invalid_command")]
  public class InvalidCommand : VsnCommand {

    public override void Execute() {
      Debug.LogWarning("Executing invalid command!");
    }

    public override void AddSupportedSignatures(){}
  }
}