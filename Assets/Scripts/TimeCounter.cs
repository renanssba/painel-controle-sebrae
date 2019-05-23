using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public enum TimeCounterAutoStart{
  none,
  autoStartCountdown,
  autoStartCountup
}

public class TimeCounter : MonoBehaviour {

  private bool countdown;
  public float maxTime;
  private float timePassed;
  public TextMeshProUGUI timeText;

  public TimeCounterAutoStart autostart;

  public UnityEvent timeOverEvent;

  private Coroutine countCoroutine;

  public static TimeCounter instance;

  public void Awake(){
    instance = this;
    switch(autostart) {
      case TimeCounterAutoStart.autoStartCountdown:
        Initialize(true, maxTime);
        break;
      case TimeCounterAutoStart.autoStartCountup:
        Initialize(false, maxTime);
        break;
    }
  }

  public void Initialize(bool isCountdown, float timeToCount){
    Debug.LogWarning("Initializing timer");
    countdown = isCountdown;
    maxTime = timeToCount;
    if(countdown) {
      StartReverseClock();
    } else {
      StartClock();
    }
  }


  public void StartReverseClock(){
    timePassed = 0;
    UpdateUI();
    countCoroutine = StartCoroutine(CountDown());
  }

  public void StartClock(){
    timePassed = 0;
    countCoroutine = StartCoroutine(CountTime());
  }

  public void StopClock(){
    StopCoroutine( countCoroutine );
  }


  public IEnumerator CountDown(){
    while(timePassed < maxTime){
      yield return null;
      //if(GameManager.instance.isRunning){
        timePassed += Time.deltaTime;
      //}
      UpdateUI();
    }
    timePassed = maxTime;
    UpdateUI();

    if(timeOverEvent != null) {
      timeOverEvent.Invoke();
    }
  }

  public IEnumerator CountTime(){
    UpdateUI();
    while(true){
      yield return null;
      timePassed += Time.deltaTime;
      UpdateUI();
    }
  }


  public void UpdateUI(){
    timeText.text = TimePassedString();
  }

  public string TimePassedString(){
    string timeString = "";
    int timeInt = (int)timePassed;
    if(countdown){
      timeInt = (int)maxTime - (int)timePassed;
    }

    timeString = (timeInt / 60).ToString();
//    if((int)(timeInt/60) < 10){
//      timeString = "0"+(int)(timeInt/60);
//    } else {
//      timeString = (timeInt / 60).ToString();
//    }

    timeString += ":";

    if(timeInt % 60 < 10) {
      timeString += "0" + (timeInt % 60);
    } else {
      timeString += (timeInt % 60).ToString();
    }

    return timeString;
  }
}
