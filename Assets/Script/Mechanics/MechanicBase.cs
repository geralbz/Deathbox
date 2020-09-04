using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MechanicBase : MonoBehaviour
{

    [SerializeField] internal int startMinute = 1;
    [SerializeField] internal int powerUpMinute = 2;
    public abstract void Activate();
    public abstract void Deactivate();
    public abstract void PowerUp();
    public abstract void PowerDown();

    public abstract void OnPlayerLose();

    internal bool activated = false;
    internal bool poweredUp = false;

    private void Awake()
    {
        Timer2.OnMinuteHasPassed += CheckShouldStart;
        Timer2.OnGameWon += Deactivate;
        Player.onPlayerDied += OnPlayerLose;
    }

    internal void Start()
    {
        StartStuff();

    }

    public void StartStuff()
    {

        if (startMinute < 1 && startMinute >= 0 && !activated)
        {
            Activate();
        }
    }

    internal virtual void CheckShouldStart(int i)
    {
        if(i >= startMinute & !activated)
        {
            activated = true;
            Activate();
        }

        if (i >= powerUpMinute & !poweredUp)
        {
            poweredUp = true;
            PowerUp();
        }
    }


    
}
