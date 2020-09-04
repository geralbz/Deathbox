using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpanderSpawner : MechanicBase
{

    public int maxSize = 30;
    public float restTime = 15;
    public float restTIme2 = 10;
    public float growthRate = 3f;
    public float shrinkFactor = 2f;
    public float expandedStayTime = 5;
    public float waitBetweenSpawns = 30f;
    public bool powered = true;
    public int damage = 1;
    bool waiting = false;
    [SerializeField]List<ExpanderDamage> exDams = new List<ExpanderDamage>();
    ExpanderDamage currentDam;

    
    public override void Activate()
    {
        activated = true;
       
        Wait();
    }

    public override void Deactivate()
    {
        foreach (ExpanderDamage dam in exDams) dam.gameObject.SetActive(false);
        activated = (startMinute == 0) ? true : false;
        PowerDown();
    }

    public override void OnPlayerLose()
    {
        //go back to normal
        currentDam.Reset();
        Deactivate();
    }

    public override void PowerDown()
    {
        foreach (ExpanderDamage dam in exDams) powered = false;
        powered = false;
    }

    public override void PowerUp()
    {
        foreach (ExpanderDamage dam in exDams) powered = true;
        powered = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(ExpanderDamage exDam in exDams)
        {
            exDam.exSpawner = this;
            exDam.maxSize = maxSize;
            exDam.growthFactor = growthRate;
            exDam.shrinkFactor = shrinkFactor;
            exDam.restTime = restTime;
            exDam.restTime2 = restTIme2;
            exDam.expandedStayTime = expandedStayTime;
            exDam.damage = damage;
            exDam.gameObject.SetActive(false);
        }
        currentDam = exDams[0];
        activated = startMinute == 0;
        base.Start();

    }

    // Update is called once per frame

    private void Update()
    {
        if (activated)
        {
            StartCircleBloat();
        }
    }
    public void StartCircleBloat()
    {
        if (exDams.Count > 0)
        {
            if (!currentDam.cycleRunning & !waiting)
            {
                currentDam = exDams[Random.Range(0, exDams.Count)];
                currentDam.gameObject.SetActive(true);
                currentDam.StartCycle();
            }
         
        }
    }

    IEnumerator Wait()
    {
        waiting = false;
        yield return new WaitForSeconds(waitBetweenSpawns);
        StartCircleBloat();
        waiting = true;
    }

    public void CycleDone()
    {
        currentDam.gameObject.SetActive(false);
        Wait();
        
    }
}
