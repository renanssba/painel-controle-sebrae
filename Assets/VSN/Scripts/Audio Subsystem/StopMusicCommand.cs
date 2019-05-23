using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command{

  [CommandAttribute(CommandString="stop_music")]
  public class StopMusicCommand : VsnCommand {

    public override void Execute(){
      VsnAudioManager.instance.StopMusic();
    }

    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[0]);
    }
  }
}