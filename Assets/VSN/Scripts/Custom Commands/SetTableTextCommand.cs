using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "set_table_text")]
  public class SetTableTextCommand : VsnCommand {

    public override void Execute() {
      StaticExecute(args);
    }

    public static void StaticExecute(VsnArgument[] args) {
      BasicController.instance.tableText.text = args[0].GetStringValue();
    }


    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.stringArg
      });
    }
  }
}