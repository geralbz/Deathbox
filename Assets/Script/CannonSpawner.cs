using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSpawner : MechanicBase
{
    [SerializeField]List<GameObject> cannons = new List<GameObject>();
    public int maxCannons = 3;
    public float spawnRate = 2f;
    int startingMaxCannon;
    float startingSpawnRate;
    public int powerUpMaxCannons = 5;
    public float powerUpSpawnRate = 1.5f;
    bool canSpawn;
    float timer;
    int activeCannons = 0;

    public override void Activate() => canSpawn = true;


    public override void Deactivate()
    {
        canSpawn = false;
        activated = false;
        activeCannons = 0;
    }

    void DeactiveCannons()
    {
       
    }

    public override void OnPlayerLose()
    {
        Deactivate();
    }

    public override void PowerUp()
    {
        maxCannons = powerUpMaxCannons;
        spawnRate = powerUpSpawnRate;
    }

    public override void PowerDown()
    {
        maxCannons = startingMaxCannon;
        spawnRate = startingSpawnRate;
    }

    // Start is called before the first frame update
    void Start()
    {
        startingMaxCannon = maxCannons;
        startingSpawnRate = spawnRate;
        DeactiveCannons();
       base.Start(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0 )
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0 & activated & canSpawn & activeCannons < maxCannons & activeCannons < cannons.Count)
        {
            SpawnCannon();
        }
    }

    public void SpawnCannon()
    {
      cannons[Random.Range(0, cannons.Count)].SetActive(true);
        activeCannons++;
        timer = spawnRate;
    }

    public void CannonLost() => activeCannons = (activeCannons - 1 >= 0)  ? activeCannons - 1 : 0;
}
