using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VsnEffectManager : MonoBehaviour {
	
  public static VsnEffectManager instance;
  public Image flashScreenImage;
  public Image fadeImage;
  public GameObject graphicsPanel;


  void Awake() {
    if(instance == null) {
      instance = this;
    }
		
  }

  public void FlashScreen(float duration) {
    flashScreenImage.GetComponent<CanvasGroup>().DOFade(1f, 0f);
    flashScreenImage.GetComponent<CanvasGroup>().DOFade(0f, duration);
  }

  public void ScreenShake(float duration, float intensity) {
    DOTween.Kill(graphicsPanel);
    graphicsPanel.transform.DOShakeRotation(duration, intensity).OnComplete( ()=>{
      graphicsPanel.transform.position = Vector3.zero;
    } );
  }

  public void FadeOut(float duration) {
    Fade(1f, duration);
  }

  public void FadeIn(float duration) {
    Fade(0f, duration);
  }

  public void Fade(float alphaValue, float duration){
    if(duration != 0) {
      fadeImage.GetComponent<CanvasGroup>().DOFade(alphaValue, duration).SetUpdate(true);
    } else {
      fadeImage.GetComponent<CanvasGroup>().alpha = alphaValue;
    }
    VsnController.instance.WaitForSeconds(duration);
  }
}
