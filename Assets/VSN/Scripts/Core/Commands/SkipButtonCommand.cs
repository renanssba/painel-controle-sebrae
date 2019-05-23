using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command{

  [CommandAttribute(CommandString="skip_button")]
  public class SkipButtonCommand : VsnCommand {

    public override void Execute (){
      
      if(args.Length > 1) {
        if(args[0].GetStringValue() == "true") {
          VsnUIManager.instance.SetSkipButtonWaypoint(args[1].GetReference());
          VsnUIManager.instance.ShowSkipButton(true);
        } else {
          Debug.LogError("Invalid args");
        }
      } else {
        if(args[0].GetStringValue() == "false") {
          VsnUIManager.instance.ShowSkipButton(false);
        } else {
          Debug.LogError("Invalid args");
        }
      }
    }

    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.stringArg,
        VsnArgType.referenceArg
      });

      signatures.Add(new VsnArgType[] {
        VsnArgType.stringArg
      });
    }
  }
}