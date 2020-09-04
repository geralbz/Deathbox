using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int hp = 1;
    public int maxHP = 10;
    public static System.Action<int> onPlayerHpChanged;
    public static System.Action onPlayerDied;
    public float damageWait = 0.5f;
    float timer;

    // Start is called before the first frame update
    void Awake()
    {
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }

    }

    public void TakeDamage(int i)
    {
        
        if (timer <= 0)
        {
            timer += damageWait;
            hp -= i;
            if (hp <= 0)
            {
                hp = maxHP;
                onPlayerDied?.Invoke();
            }
            // Debug.Log(hp);
            onPlayerHpChanged?.Invoke(hp);
           
        }
    }
}
