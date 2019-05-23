using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command{

  [CommandAttribute(CommandString="play_music")]
  public class PlayMusicCommand : VsnCommand {

    public override void Execute(){
      if(args.Length >= 2){
        VsnAudioManager.instance.PlayMusic(args[0].GetStringValue(), args[1].GetStringValue());
      } else {
        VsnAudioManager.instance.PlayMusic(null, args[0].GetStringValue());
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
    }
  }
}