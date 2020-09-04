using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public GameObject target;      
  // [SerializeField]private Vector3 offset;
    [SerializeField] Vector2 safeFollowDist = new Vector2(8f, 5f);
    [SerializeField] float spd = 0.1f;
    [SerializeField] float camDist = 10f;
    [SerializeField] bool smooth = false;
    [SerializeField] bool follow = false;
    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        //offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (follow)
        {
            if (Mathf.Abs(transform.position.y - target.transform.position.y) > safeFollowDist.y ||
                Mathf.Abs(transform.position.x - target.transform.position.x) > safeFollowDist.x)
            {
                Vector3 desiredPosition = target.transform.position;
                desiredPosition.z = camDist * -1;
                transform.position = desiredPosition;
            }
        }
        else if (smooth)
        {
            Vector3 desiredPosition = target.transform.position;
            desiredPosition.z = camDist * -1;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, spd * Time.deltaTime);
        }
    }
}

