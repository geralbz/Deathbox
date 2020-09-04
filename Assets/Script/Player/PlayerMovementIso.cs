using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementIso : MonoBehaviour
{
    public float spd = 30f;
    //public float fireRate = 0.5f;
    float fireTime;
    float hor;
    float vert;
    Vector2 movement;
    Rigidbody2D rb;

    bool facingRight = true;
    [SerializeField]
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hor = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        facingRight = hor < 0 || facingRight & hor == 0;
        // hor = facingRight ? hor : -hor;

        movement = new Vector2(hor, vert);
        ///transform.Translate(movement * spd * Time.deltaTime);
        //rb.AddForce(movement * spd * Time.deltaTime);
        rb.velocity = movement * spd * Time.deltaTime;

    }

}
