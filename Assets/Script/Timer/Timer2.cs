using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer2 : MonoBehaviour
{
    //public Text timerText;
    public TextMeshProUGUI timerText;
    public float startTime;
    float timer;
    public static System.Action<int> OnMinuteHasPassed;
    public static System.Action OnGameWon;
    public static bool gameWon = false;
    public int winScene;
    bool pause = false;
    float timeDif;

    [SerializeField] public int Minutes { get; private set; } = 0;
    private void Awake()
    {
        InitTimer();

    }

    public void GoToWin() => SceneManager.LoadScene(winScene);
    private void Start()
    {
        Player.onPlayerDied += DeathPenalty;
        OnGameWon += GoToWin;

        timer = startTime;
    }
    void Update()
    {
        //or game is paused
        if (!gameWon)
        {
            UpdateTimerUI();
        }
    }

    public string SetTime() => timerText.text = (timer).ToString("0");

    public void DeathPenalty()
    {
        Minutes = 0;
        timer = startTime;
        timerText.text = SetTime();
        //Debug.Break();
    }

    public void InitTimer()
    {
        timer = startTime;

    }

    //call this on update
    public void UpdateTimerUI()
    {

        //set timer UI
        timer -= Time.deltaTime;
        SetTime();

        timeDif = (startTime - timer) / 60;

        if (timeDif >= Minutes + 1)
        {
            Minutes = Minutes + 1;
            OnMinuteHasPassed?.Invoke(Minutes);
            //Debug.Log("minute is up");
        }

        if (timer <= 0 & !pause)
        {
            timer = 0;
            OnGameWon?.Invoke();
        }

    }


}
