using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MechanicBase
{
    public string projectileRef = "Straight_Projectile";
    public Vector2 fireRateRange = new Vector2(0.5f, 1f);
    public float spd = 300f;
    public float powerUpSpd = 600;
    public float lifeTime = 10f;
    public int damage = 1;
    public int powerUpDamage = 3;
    public float powerUpLifeTime = 15f;
    float startingspd, startingPowerUpLifeTime;
    int startingDam;
    Vector2 startingfireRate;
    public Vector2 poweredUpFireRateRange = new Vector2(0.8f, 0.9f);
    bool firing = false;
    float timer;
    float disableTimer;
    Transform playerTrans;
    [SerializeField]CannonSpawner spawner;

    
    private void Start()
    {
        startingDam = damage;
        startingfireRate = fireRateRange;
        startingspd = spd;
        startingPowerUpLifeTime = lifeTime;
        playerTrans = GameObject.FindObjectOfType<Player>().transform;
        Player.onPlayerDied += Disable;
        base.Start();
    }

    void Disable() => gameObject.SetActive(false);
    private void OnEnable()
    {
        if(GameObject.FindObjectOfType<Timer2>().Minutes >= powerUpMinute & !poweredUp)
        {
            PowerUp();
        }
        timer = fireRateRange.x;
    }
    public override void Activate()
    {

    }

    public override void Deactivate()
    {
        PowerDown();
   
    }

    public override void PowerDown()
    {
        spd = startingspd;
        fireRateRange = startingfireRate;
        damage = startingDam;
        lifeTime = startingPowerUpLifeTime;
    }

    public override void PowerUp()
    {
        spd = powerUpSpd;
        fireRateRange = poweredUpFireRateRange;
        damage = powerUpDamage;
        lifeTime = powerUpLifeTime;
    }

    public override void OnPlayerLose()
    {
        PowerDown();
        if (startMinute != 0)
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
        if (timer <= 0)
        {
            Fire();
        }
    }

    void Fire()
    {

            GameObject go = Instantiate(Resources.Load<GameObject>("Projectiles/" + projectileRef), transform.position, transform.rotation);
            go.transform.parent = gameObject.transform;
            Vector2 dir = playerTrans.position  - transform.position;
            go.GetComponent<StraightProjectile>().damage = damage;
            go.GetComponent<Rigidbody2D>().velocity = dir * spd * Time.deltaTime;
            timer = Random.Range(fireRateRange.x, fireRateRange.y);

    }

    private void OnDisable()
    {
        if(spawner == null)
        {
            spawner = GameObject.FindObjectOfType<CannonSpawner>();
        }
       
        spawner.CannonLost();
    }
    private void OnDestroy()
    {
        Player.onPlayerDied -= Disable;
    }
}
