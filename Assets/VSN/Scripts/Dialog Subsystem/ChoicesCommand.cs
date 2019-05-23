using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "choices")]
  public class ChoicesCommand : VsnCommand {

    public override void Execute() {
      string[] choices = new string[args.Length/2];
      string[] labels = new string[args.Length/2];

      for(int i=0; i < args.Length/2; i++) {
        choices[i] = args[i*2].GetStringValue();
        labels[i] = args[i*2+1].GetReference();
      }

      VsnUIManager.instance.SetChoicesTexts(choices);
      VsnUIManager.instance.SetChoicesLabels(labels);
      VsnUIManager.instance.ShowChoicesPanel(true, choices.Length);

      VsnUIManager.instance.ShowDialogPanel(true);
      VsnController.instance.state = ExecutionState.WAITINGCHOICE;
    }


    public override void AddSupportedSignatures() {
      VsnArgType[] argTypes;

      for(int i=1; i<=6; i++){
        argTypes = new VsnArgType[i*2];
        for(int j=0; j<i; j++){
          argTypes[j*2] = VsnArgType.stringArg;
          argTypes[j*2+1] = VsnArgType.referenceArg;
        }
        signatures.Add(argTypes);
      }
    }
  }
}