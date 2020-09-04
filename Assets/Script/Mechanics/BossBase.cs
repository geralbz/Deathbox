using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBase : MechanicBase
{
    public int bossEndMin = 5;
    [HideInInspector]
     new int powerUpMinute;
    [SerializeField]internal GameObject bossBody;

    private void Start()
    {
        Timer2.OnMinuteHasPassed += EndBossFight;
        base.Start();
    }

    void EndBossFight(int i)
    {

        if(i >= bossEndMin)
        {
            Deactivate();

        }
    }

    

    public override void Activate()
    {
        activated = true;
        bossBody.SetActive(true);
    }

    public override void Deactivate()
    {
        activated = false;
        bossBody.SetActive(false);
    }

    public override void OnPlayerLose()
    {
        Deactivate();
    }

    internal override void CheckShouldStart(int i)
    {
        if (i >= startMinute & !activated & i <= bossEndMin)
        {
            activated = true;
            Activate();
        }


    }

    public override void PowerDown()
    {
        
    }

    public override void PowerUp()
    {
       
    }

}


