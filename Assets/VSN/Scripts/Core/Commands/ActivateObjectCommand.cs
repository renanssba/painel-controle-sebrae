using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "activate_object")]
  public class ActivateObjectCommand : VsnCommand {

    public override void Execute() {
      Transform t = VsnController.instance.transform.parent.Find(args[0].GetStringValue());

      if(args[1].GetStringValue() == "true") {
        t.gameObject.SetActive(true);
      } else {
        t.gameObject.SetActive(false);
      }
    }


    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.stringArg,
        VsnArgType.stringArg
      });
    }
  }
}