using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PartialLoopPlayer : MonoBehaviour {
	
  public AudioSource introSource;
  public AudioSource loopSource;
  public bool autoPlay = false;

  private bool started = false;


  void Start() {
    if(autoPlay)
      StartPlaying();
  }


  public void SetMusic(AudioClip intro, AudioClip loop) {
    StopPlaying();
		
    introSource.clip = intro;
    loopSource.clip = loop;
		
//		StartPlaying();
  }

  public void StartPlaying() {
    FadeMusic(1f, 0f);
    if(introSource.clip != null) {
      introSource.time = 0f;
      introSource.Play();
      loopSource.time = 0f;
      loopSource.PlayDelayed(introSource.clip.length);
//      StartCoroutine(WaitIntroEnd());
    } else if(loopSource.clip != null) {
      loopSource.time = 0f;
      loopSource.Play();
    }
    started = true;
  }

  public void StopPlaying(){
    introSource.Stop();
    loopSource.Stop();
    StopAllCoroutines();
  }

  public IEnumerator WaitIntroEnd(){
    while(introSource.isPlaying){
      yield return null;
    }
    if(!loopSource.isPlaying && loopSource.clip != null){
      loopSource.time = 0f;
      loopSource.Play();
    }
  }



  public void PlayFromTime(float startTime){
    introSource.volume = 0f; 
    loopSource.volume = 0f; 
    FadeMusic(0.5f, 0.8f); 

    if(introSource.clip != null){ 
      if(startTime >= introSource.clip.length){ 
        startTime -= introSource.clip.length; 
      }else{ 
        introSource.time = startTime; 
        introSource.Play(); 
        loopSource.PlayDelayed(introSource.clip.length - startTime); 
        return; 
      } 
    } 

    loopSource.time = startTime; 
    loopSource.Play(); 
  } 

  public float GetPlayedTime(){ 
    float playedTime; 

    if(introSource.isPlaying){ 
      return introSource.time; 
    }else if(loopSource.isPlaying){ 
      playedTime = loopSource.time; 
      if(introSource.clip != null){ 
        playedTime += introSource.clip.length; 
      } 
      return playedTime; 
    } 
    return 0; 
  } 

  public void FadeMusic(float fadeValue, float fadeTime){
    //Debug.LogWarning("Fading music volume to " + fadeValue + " in " + fadeTime + " seconds.");
    DOTween.Kill(introSource); 
    DOTween.Kill(loopSource); 

    if(fadeTime > 0f) {
      introSource.DOFade(fadeValue, fadeTime); 
      loopSource.DOFade(fadeValue, fadeTime); 
    } else {
      introSource.volume = fadeValue;
      loopSource.volume = fadeValue;
    }
  }	
}
