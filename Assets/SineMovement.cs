using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SineMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 4;
    public int damage = 1;
    public float lifeTime = 20f;
    public float wave = 4;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject.Destroy(gameObject, lifeTime);
        Player.onPlayerDied += DestroyNow;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(wave * Mathf.Sin(speed * Time.time), 0);
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
