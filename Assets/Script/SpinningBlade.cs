using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBlade : MonoBehaviour
{
    public float rotation = 20f;
    public float rotation2 = 40f;
    float myRot;
    public bool poweredUp = false;
    private void Update()
    {
        myRot = poweredUp ? rotation : rotation2;
        transform.Rotate(0, 0, rotation * Time.deltaTime);
    }

    private void OnDisable()
    {
        poweredUp = false;
    }
}