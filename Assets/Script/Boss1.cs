using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : BossBase
{
   // public float roatationSpd = 1f;
    public float rotation = 3f;
    public Vector2 timeBeforeFiring = new Vector2(2f, 5f);
    public float preFireWait = 1f;
    public float postFireWait = 1f;

    public string projectileRef = "Straight_Projectile";
    public float spd = 300f;
    public int damage = 1;
    public float lifeTime = 8f;
    public List<FirePointDirection> firePts = new List<FirePointDirection>();
    public GameObject rejectionPt;

    bool waiting = false;
    public enum STATE { ROTATION, PREFIRE, FIRE, POSTFIRE}
    public STATE myState = STATE.ROTATION;

    // Update is called once per frame
    void Update()
    {
        //rotate clockwise or counterclockwise and fire a bouncing projectiles in every direction
        if (activated)
        {
            switch (myState)
            {
                case STATE.ROTATION:
                    if (!waiting)
                    {
                        StartCoroutine(Wait(Mathf.RoundToInt(Random.Range(timeBeforeFiring.x, timeBeforeFiring.y)), STATE.PREFIRE));
                    }
                    bossBody.transform.Rotate(0, 0, rotation * Time.deltaTime);
                    break;

                case STATE.PREFIRE:
                    if (!waiting)
                        StartCoroutine(Wait(preFireWait, STATE.FIRE));
                    break;

                case STATE.FIRE:
                    Fire();
                    break;

                case STATE.POSTFIRE:
                    if (!waiting)
                        StartCoroutine(Wait(postFireWait, STATE.ROTATION));
                    break;
            }
        }
    }
    void Fire()
    {
        if (firePts.Count > 0)
        {
            foreach (FirePointDirection firept in firePts)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("Projectiles/" + projectileRef), firept.transform.position, firept.transform.rotation);
                go.transform.parent = gameObject.transform;
                Vector2 dir = GetRotationVector(firept.transform.position);//firept.GetFireDir();
                go.GetComponent<BounceProjectile>().damage = damage;
                go.GetComponent<BounceProjectile>().lifeTime = lifeTime;

                go.GetComponent<Rigidbody2D>().velocity = dir * spd * Time.deltaTime;
            }
        }
        myState = STATE.POSTFIRE;
    }

    IEnumerator Wait(float f, STATE nextState = STATE.ROTATION)
    {
        waiting = true;
        yield return new WaitForSeconds(f);
        myState = nextState;
        waiting = false;
    }

    public Vector2 GetRotationVector(Vector2 pos)
    {
        //calculate  the vector from firepoint to rejection point and then reverse it.
        Vector2 newDir = pos - (Vector2)rejectionPt.transform.position;

        return newDir;

    }
}
