using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MechanicManager))]
public class StraightProjectileMechanic : MechanicBase
{
    public string projectileRef = "Straight_Projectile";
    public Vector2 fireRateRange = new Vector2(0.5f, 1f);
    public float spd = 300f;
    public float powerUpSpd = 600;
    public int damage = 1;
    public int powerUpDamage = 3;
    public int concurrentFire = 1;
    public int powerUpConcurrentFire = 2;
    float startingspd;
    int startingDam;
    int startingCurrent;
    Vector2 startingfireRate;
    public Vector2 poweredUpFireRateRange = new Vector2(0.8f, 0.9f);
    public List<FirePointDirection> firePts = new List<FirePointDirection>();
    public List<FirePointDirection> firePtsToUse = new List<FirePointDirection>();
    bool firing = false;
    float timer;

    int previousFire = -1;
    private void Start()
    {
       startingDam = damage;
       startingfireRate = fireRateRange;
        startingspd = spd;
        startingCurrent = concurrentFire;
        base.Start();
    }

    public override void Activate()
    {
        activated = true;

    }

    public override void Deactivate()
    {
        activated = false;
        previousFire = -1;
        PowerDown();
    }

    public override void PowerDown()
    {
        spd = startingspd;
        fireRateRange = startingfireRate;
        damage = startingDam;
        concurrentFire = startingCurrent;
    }

    public override void PowerUp()
    {
        spd = powerUpSpd;
        fireRateRange = poweredUpFireRateRange;
        damage = powerUpDamage;
        concurrentFire = powerUpConcurrentFire;
    }

    public override void OnPlayerLose()
    {
        PowerDown();
        if(startMinute != 0)
        {
            Deactivate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0 & activated)
        { 
            Fire();
        }
    }

    void Fire()
    {
        if (firePts.Count > 0)
        {
            if(firePts.Count < concurrentFire)
            {
                concurrentFire = firePts.Count;
            }
            firePtsToUse.Clear();
            do
            {

                firePtsToUse.Add(firePts[(Random.Range(0, firePts.Count))]);

            } while (firePtsToUse.Count < concurrentFire);

            
            for (int i = 0; i < concurrentFire; i++)
            {
                if (i == previousFire)
                {
                    previousFire = -1;
                    continue;
                }
                previousFire = i;
                GameObject go = Instantiate(Resources.Load<GameObject>("Projectiles/" + projectileRef), firePtsToUse[i].transform.position, firePtsToUse[i].transform.rotation);
                go.transform.parent = gameObject.transform;
                Vector2 dir = firePtsToUse[i].GetFireDir();
                go.GetComponent<StraightProjectile>().damage = damage;

                go.GetComponent<Rigidbody2D>().velocity = dir * spd * Time.deltaTime;
                timer = Random.Range(fireRateRange.x, fireRateRange.y);
            }
                    
        }
    }
}
