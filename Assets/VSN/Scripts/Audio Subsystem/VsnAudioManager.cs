using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VsnAudioManager : MonoBehaviour {

	public static VsnAudioManager instance;

	private AudioSource audioSource;
  public PartialLoopPlayer musicPlayer;
  public AudioSource[] ambienceSources;
  public AudioClip dialogSfx = null;
  public float dialogSfxTime = 0.08f;

  public string songPlaying;
  public AudioClip lastSongPlayingIntro;
  public AudioClip lastSongPlayingLoop;
  public float lastSongTimePlayed;
  public string[] pathsToBuffer;
  public List<AudioClip> bufferedAudioClips;


	void Awake(){
		audioSource = GetComponent<AudioSource>();
    bufferedAudioClips = new List<AudioClip>();

    if(instance == null) {
      instance = this;
      DontDestroyOnLoad(gameObject);
    } else if(instance != this){
      Destroy(gameObject);
    }
    BufferAudioPaths();
	}

  void Start(){
//    Debug.Log("loading volume");
    LoadVolume();
//    BufferAudioPaths();
  }

  public void LoadVolume(){
    SetVolume(VsnSaveSystem.GetFloatVariable("audio", 1f));
  }

  void BufferAudioPaths(){
    foreach(string currentPath in pathsToBuffer){
//      Debug.LogWarning("Buffering path Resources/" + currentPath);
      bufferedAudioClips.AddRange( Resources.LoadAll<AudioClip>(currentPath) );
    }
  }

  public void BufferAudioClip(string audioPath){
    bufferedAudioClips.Add(Resources.Load<AudioClip>(audioPath));
  }

  public void UnbufferAudioClip(string clipName) {
    for (int i = 0; i < bufferedAudioClips.Count; i++) {
      if (bufferedAudioClips[i].name == clipName) {
        bufferedAudioClips.RemoveAt(i);
        return;
      }
    }
  }

  AudioClip GetBufferedClip(string clipName){
    foreach(AudioClip clip in bufferedAudioClips){
      if(clip.name == clipName){
        return clip;
      }
    }
    return null;
  }

  public AudioClip GetAudioClip(string clipName){
    if(clipName == null){
      return null;
    }
  
    AudioClip clip = null;
    clip = GetBufferedClip(clipName);
    if(clip == null) {
      clip = Resources.Load<AudioClip>(clipName);
      if(clip == null) {
        Debug.LogError("Error loading audio clip: " + clipName + ". Please check the provided path.");
      }
    }
    return clip;
  }


  public void PrepareMusic(string introMusic, string loopMusic, bool storeLastPlayed = true){
    if(introMusic == null &&
       loopMusic == null){
      Debug.LogWarning("Prepare: Setting music to null.");
      return;
    }

    AudioClip introClip = GetAudioClip(introMusic);
    AudioClip loopClip = GetAudioClip(loopMusic);
    if(loopClip != null) {
      songPlaying = loopClip.name;
    }

    FadeMusic(1f, 0f);
    musicPlayer.SetMusic(introClip, loopClip);

    if(storeLastPlayed){
      StoreLastSongPlayed();
    }
  }


  public void PlayMusic(string introMusic, string loopMusic, bool storeLastPlayed = true){
    //    Debug.LogWarning("Trying to play music: " + introMusic + " and " + loopMusic);
    if (GetPlayingMusicName() == GetNameFromPath(loopMusic) ) {
      Debug.LogWarning("wont play the same music");
      return;
    }

    if(introMusic == null &&
       loopMusic == null){
      Debug.LogWarning("Setting music to null.");
      return;
    }

    AudioClip introClip = GetAudioClip(introMusic);
    AudioClip loopClip = GetAudioClip(loopMusic);
    if(loopClip != null) {
      songPlaying = loopClip.name;
    }

    FadeMusic(1f, 0f);
    musicPlayer.SetMusic(introClip, loopClip);
    musicPlayer.StartPlaying();

    if(storeLastPlayed){
      StoreLastSongPlayed();
    }
  }

  public string GetNameFromPath(string songPath){
    if(songPath != null){
      char delimiter = '/';
      string[] parts = songPath.Split(delimiter);
      return parts[parts.Length-1];
    }
    return null;
  }

  public void StopMusic(){
    musicPlayer.StopPlaying();
  }

  public string GetPlayingMusicName(){
    if(musicPlayer.loopSource.clip != null &&
       (musicPlayer.loopSource.isPlaying || musicPlayer.introSource.isPlaying)
      ){
      return musicPlayer.loopSource.clip.name;
    }
    return "";
  }

  public void PlaySfx(string clipName){
    Debug.LogWarning("PLAY: " + clipName);
    PlaySfx(clipName, 0f);
  }

  public void FadeMusic(float fadeValue, float fadeTime) {
    musicPlayer.FadeMusic(fadeValue, fadeTime);
  }

  public void PlaySfx(string clipName, float panValue){
    AudioClip audioClip = GetAudioClip(clipName);
    if(audioClip == null){
      Debug.LogError("Error loading " + clipName + " sfx. Please check its path");
      return;
    }
    PlaySfx(audioClip, panValue);
  }

  public void PlaySfx(AudioClip audioClip){
    PlaySfx(audioClip, 0f);
  }

  public void PlaySfx(AudioClip audioClip, float panValue){
    /// TODO: add support to sfx panning
    audioSource.panStereo = 0f;
    audioSource.PlayOneShot(audioClip);
	}


  public void PlayDialogSfx(){
    if(dialogSfx != null){
      audioSource.PlayOneShot(dialogSfx);
    }
  }

  public void PlayAmbience(string clipName){
    AudioClip audioClip = GetAudioClip(clipName);
    if(audioClip == null){
      Debug.LogError("Error loading " + clipName + " ambience sfx. Please check its path");
      return;
    }
    AudioSource s = GetAvailableAmbienceSource();
    s.clip = audioClip;
    s.Play();
  }

  public void StopAmbience(string clipName){
    foreach(AudioSource s in ambienceSources){
      if(s.clip != null && s.clip.name == clipName){
        s.Stop();
        s.clip = null;
      }
    }
  }


  AudioSource GetAvailableAmbienceSource(){
    for(int i=0; i<ambienceSources.Length; i++){
      if( !ambienceSources[i].isPlaying )
        return ambienceSources[i];
    }
    return null;
  }

  public void SetVolume(float volume){
    audioSource.volume = volume;
    musicPlayer.introSource.volume = volume;
    musicPlayer.loopSource.volume = volume;
    foreach(AudioSource s in ambienceSources){
      s.volume = volume;
    }
  }

  public void ResumeLastSong() {
    musicPlayer.SetMusic(lastSongPlayingIntro, lastSongPlayingLoop);
    musicPlayer.PlayFromTime(lastSongTimePlayed);
  }

  public void StoreLastSongPlayed() {
    lastSongPlayingIntro = musicPlayer.introSource.clip;
    lastSongPlayingLoop = musicPlayer.loopSource.clip;   
  }

  public void StoreLastSongPlayedTime() {
    lastSongTimePlayed = musicPlayer.GetPlayedTime(); 
  }
}
