using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "subtract_var")]
  public class SubtractVariableCommand : VsnCommand {

    public override void Execute() {
      VsnArgument variableName = args[0];
      VsnArgument valueToAdd = args[1];

      float oldValue = VsnSaveSystem.GetFloatVariable(variableName.GetReference());
      float newValue = oldValue - valueToAdd.GetNumberValue();

      VsnSaveSystem.SetVariable(variableName.GetReference(), newValue);
      VsnSaveSystem.Save(0);
    }


    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.referenceArg,
        VsnArgType.numberArg
      });
    }
  }
}