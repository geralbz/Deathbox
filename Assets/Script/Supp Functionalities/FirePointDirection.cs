using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointDirection : MonoBehaviour
{
    public enum FireDIR { RIGHT, LEFT, UP, DOWN, UPPERLEFT, UPPERRIGHT, BOTTOMLEFT, BOTTOMRIGHT };
    public FireDIR myFireDirection = FireDIR.RIGHT;

    public Vector2 GetFireDir()
    {
        Vector2 dir;
        switch (myFireDirection)
        {
            case FirePointDirection.FireDIR.RIGHT:
                dir = new Vector2(1, 0);
                break;

            case FirePointDirection.FireDIR.LEFT:
                dir = new Vector2(-1, 0);
                break;

            case FirePointDirection.FireDIR.UP:
                dir = new Vector2(0, 1);
                break;

            case FirePointDirection.FireDIR.DOWN:
                dir = new Vector2(0, -1);
                break;

            case FirePointDirection.FireDIR.UPPERLEFT:
                dir = new Vector2(1, -1);
                break;

            case FirePointDirection.FireDIR.UPPERRIGHT:
                dir = new Vector2(1, 1);
                break;

            case FirePointDirection.FireDIR.BOTTOMLEFT:
                dir = new Vector2(-1, -1);
                break;

            case FirePointDirection.FireDIR.BOTTOMRIGHT:
                dir = new Vector2(-1, 1);
                break;
            default:
                dir = new Vector2(1, 0);
                break;
        }
        return dir;
    }
}
