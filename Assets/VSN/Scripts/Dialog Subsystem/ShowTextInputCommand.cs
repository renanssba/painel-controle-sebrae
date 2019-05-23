using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "show_text_input")]
  public class ShowTextInputCommand : VsnCommand {

    public override void Execute() {
      VsnController.instance.state = ExecutionState.WAITINGTEXTINPUT;

      if(args[0].GetStringValue() == "true") {
        if(args.Length > 1) {
          VsnUIManager.instance.SetTextInputDescription(args[1].GetStringValue());
        }
        if(args.Length > 2) {
          VsnUIManager.instance.SetTextInputCharacterLimit((int)args[2].GetNumberValue());
        } else {
          VsnUIManager.instance.SetTextInputCharacterLimit(0);
        }
        VsnUIManager.instance.ShowTextInput(true);
      } else if(args[0].GetStringValue() == "true") {
        VsnUIManager.instance.ShowTextInput(false);
      } else {
        Debug.LogError("Invalid value for argument: " + args[0].GetStringValue());
      }
    }


    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.stringArg
      });

      signatures.Add(new VsnArgType[] {
        VsnArgType.stringArg,
        VsnArgType.stringArg
      });

      signatures.Add(new VsnArgType[] {
        VsnArgType.stringArg,
        VsnArgType.stringArg,
        VsnArgType.numberArg
      });
    }
  }
}