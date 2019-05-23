using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command {

  [CommandAttribute(CommandString = "character")]
  public class CharacterCommand : VsnCommand {

    public override void Execute() {
      string characterLabel = args[0].GetReference();
      string characterFilename = args[1].GetStringValue();

      Sprite characterSprite = Resources.Load<Sprite>("Characters/" + characterFilename);
      if(characterSprite == null){
        Debug.LogError("Error loading " + characterFilename + " character sprite. Please check its path");
        return;
      }
      VsnUIManager.instance.CreateNewCharacter(characterSprite, characterFilename, characterLabel);
    }

    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.referenceArg,
        VsnArgType.stringArg
      });
    }

  }
}