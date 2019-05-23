using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "exit_game")]
  public class ExitGameCommand : VsnCommand {

    public override void Execute() {
      Application.Quit();
    }

    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[0]);
    }
  }
}