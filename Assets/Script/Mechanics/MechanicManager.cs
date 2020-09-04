using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicManager : MonoBehaviour
{
    [SerializeField]List<MechanicBase> mechanics = new List<MechanicBase>();

    private void Start()
    {
        gameObject.GetComponentsInChildren<MechanicBase>();

        Timer.OnGameWon += ClearMechanics;
    }

    public void MechanicTimeAssignment()
    {

    }

    public void ClearMechanics()
    {
        foreach(MechanicBase mechanic in mechanics)
        {
            mechanic.Deactivate();
        }
    }
}
