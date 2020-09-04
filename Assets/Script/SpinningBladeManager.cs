using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBladeManager : MechanicBase
{
    public float startSpd = 20f;
    public float powerUpSpd = 40f;
    public List<SpinningBlade> blades = new List<SpinningBlade>();
    [SerializeField] bool triggerAll = true;
    public override void Activate()
    {
        if (blades.Count > 0)
        {
            if (triggerAll)
            {
                foreach (SpinningBlade blade in blades)
                {
                    blade.rotation = startSpd;
                    blade.rotation2 = powerUpSpd;
                    blade.gameObject.SetActive(true);
                }
            }
            else
            {
                List<int> rands = new List<int>();
                int bladesToActivate = Mathf.CeilToInt(blades.Count / 2);
                while (rands.Count < bladesToActivate)
                {
                    rands.Add(Random.Range(0, blades.Count));
                }

                foreach (int i in rands)
                {
                    blades[i].rotation = startSpd;
                    blades[i].rotation2 = powerUpSpd;
                    blades[i].gameObject.SetActive(true);

                }
            }
        };
    }

    public override void Deactivate()
    {
        foreach (SpinningBlade blade in blades) blade.gameObject.SetActive(false);
        activated = false;
    }

    public override void OnPlayerLose()
    {
        foreach (SpinningBlade blade in blades) blade.gameObject.SetActive(false);
    }

    public override void PowerDown()
    {
        foreach (SpinningBlade blade in blades) blade.poweredUp = false;
    }

    public override void PowerUp()
    {
        foreach (SpinningBlade blade in blades) blade.poweredUp = true;
    }


}
