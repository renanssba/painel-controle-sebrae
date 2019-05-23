using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Command{

	[CommandAttribute(CommandString = "load_scene")]
	public class LoadSceneCommand : VsnCommand {

		public override void Execute (){
      SceneManager.LoadScene(args[0].GetStringValue());
		}

    public override void AddSupportedSignatures() {
      signatures.Add(new VsnArgType[] {
        VsnArgType.stringArg
      });
    }
	}
}