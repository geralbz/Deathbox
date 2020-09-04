using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpanderDamage : MonoBehaviour
{
    public float maxSize = 20;
    public Vector2 startingSize;
    public float restTime = 15;
    public float restTime2 = 30f;
    public float growthFactor = 3f;
    public float shrinkFactor = 2f;
    public float expandedStayTime = 5;
    public int damage = 1;
    public float damageWait = 1;
    float timer = 1;

    bool powered = true;
    bool resting = false;
    bool expanding = false;
    bool shrinking = false;
    [SerializeField] bool testing = false;
    [SerializeField] public bool cycleRunning { get; private set; } = false;
    public ExpanderSpawner exSpawner;

    [SerializeField] Vector2 destinationScale;
    float time;
    float currentTime;
    public enum STATE { REST, EXPANDING, BLOATED, SHRINKING }
    public STATE myState = STATE.REST;

    // Start is called before the first frame update
    void Start()
    {
        startingSize = transform.localScale;

    }

    public void StartCycle() => cycleRunning = true;

    // Update is called once per frame
    void Update()
    {
        float trueRest = powered ? restTime : restTime2;
        if (cycleRunning || testing)
        {
            switch (myState)
            {
                case STATE.REST:
                    // Debug.Log("in rest");
                    if (!resting)
                        StartCoroutine(Wait(trueRest, STATE.EXPANDING));
                    break;

                case STATE.EXPANDING:

                    if (!expanding)
                    {
                        destinationScale = new Vector2(startingSize.x * maxSize, startingSize.y * maxSize);
                        StartCoroutine(Expand(destinationScale));
                    }

                    break;

                case STATE.BLOATED:
                    StartCoroutine(Wait(expandedStayTime, STATE.SHRINKING));
                    break;

                case STATE.SHRINKING:
                    if (!shrinking)
                    {
                        destinationScale = new Vector2(startingSize.x, startingSize.y);
                        StartCoroutine(Shrinking());
                    }
                    break;
            }
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 0;
            }
        }
    }


    IEnumerator Wait(float f, STATE nextState = STATE.REST)
    {

        resting = true;
        yield return new WaitForSeconds(f);
        myState = nextState;
        resting = false;
    }

    IEnumerator Expand(Vector2 destScale)
    {
        expanding = true;

        currentTime = 0;

        while (!Equals((Vector2)transform.localScale, destScale))
        {
            if (Pausing.pause)
            {
                yield return 0;
            }
            else
            {
                transform.localScale = Vector2.Lerp(transform.localScale, destScale, Mathf.Min(1, (currentTime * growthFactor) / Time.time));
                currentTime += Time.deltaTime;
                CheckExpandScale();
                yield return null;
            }
        }


    }
    void CheckExpandScale()
    {
        //Debug.Log((transform.localScale.x >= maxSize - 0.1) + "me over max is " + transform.localScale.x + " >= " + maxSize);
        if (transform.localScale.x >= maxSize - 0.001)
        {
            transform.localScale = new Vector2(maxSize, maxSize);
            myState = STATE.BLOATED;
            expanding = false;

        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {

    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (timer <= 0)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
                timer = damageWait;
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {

    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timer = 0;
        }
    }

    void CheckShrinkScale()
    {
        if (transform.localScale.x <= startingSize.x + 0.001)
        {
            transform.localScale = startingSize;
            StopAllCoroutines();
            exSpawner?.CycleDone();
            myState = STATE.REST;
            shrinking = false;
            cycleRunning = false;

        }

    }


    IEnumerator Shrinking()
    {
        shrinking = true;


        while (!Equals((Vector2)transform.localScale, destinationScale))
        {
            if (Pausing.pause)
            {
                yield return null;
            }
            else
            {
                transform.localScale = Vector2.Lerp(transform.localScale, destinationScale, Mathf.Min(1, (currentTime * shrinkFactor) / Time.time));
                currentTime += Time.deltaTime;
                CheckShrinkScale();
                yield return null;
            }
        }

    }

    public void Reset()
    {
        transform.localScale = startingSize;
        myState = STATE.REST;
        cycleRunning = false;
    }
}


