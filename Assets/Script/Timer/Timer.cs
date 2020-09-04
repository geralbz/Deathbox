using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //public Text timerText;
    public TextMeshProUGUI timerText;
    private float secondsCount;
    private int minuteCount;
    private int hourCount;
    public float startTime;
    public static System.Action<int> OnMinuteHasPassed;
    public static System.Action OnGameWon;
    public static bool gameWon = false;
    public int goalTimeInMinutes = 60;
    public int winScene;
    int minutesPassed = 0;

    public int Minutes => minuteCount;
    private void Awake()
    {
        InitTimer();

    }

    public void GoToWin() => SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(winScene).name);
    private void Start()
    {
        Player.onPlayerDied += DeathPenalty;
        OnGameWon += GoToWin;
  
        SetTime(0, goalTimeInMinutes, 0);
    }
    void Update()
    {
        //or game is paused
        if (!gameWon)
        {
            UpdateTimerUI();
        }
    }

    public string SetTime(int h, int m, float s) => h + "h:" + m.ToString("00") + "m:" + ((int)s).ToString("00") + "s";

    public void DeathPenalty()
    {
        minutesPassed = 0;
        timerText.text = SetTime(0,goalTimeInMinutes,0);
        //Debug.Break();
    }

    public void InitTimer()
    {
        minuteCount = 0;
        hourCount = 0;

    }

    //call this on update
    public void UpdateTimerUI()
    {
        if (minuteCount < goalTimeInMinutes)
        {
            //set timer UI
            secondsCount -= Time.deltaTime - startTime;


            if (secondsCount <= 0)
            {
                minuteCount--;
                secondsCount = 60;
                minutesPassed++;
                OnMinuteHasPassed?.Invoke(minutesPassed);
            }
            timerText.text = SetTime(hourCount, minuteCount, secondsCount);
        }
        //5 should be 60. this says 5 minutes is an hour
        else if (minutesPassed >= goalTimeInMinutes)
        {
            hourCount++;
            minuteCount = 0;
            OnGameWon?.Invoke();
            gameWon = true;
            timerText.text = SetTime(hourCount, minuteCount, secondsCount);
        }
    }
}


