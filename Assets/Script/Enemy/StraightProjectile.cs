using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 5f;
   

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, lifeTime);
        // Player.onPlayerDied += () => GameObject.Destroy(gameObject);
        Player.onPlayerDied += DestroyNow ;
    }

    void DestroyNow() => GameObject.Destroy(gameObject);
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            DestroyNow();
        }

    }

    private void OnDestroy()
    {
        Player.onPlayerDied -= DestroyNow;
    }
}
