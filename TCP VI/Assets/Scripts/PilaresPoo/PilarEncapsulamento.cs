using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarEncapsulamento : POOPillar
{
    [SerializeField] MyFirstClassManager myFirstClassManager;

    public void Initialize()
    {
        if (HasWon)
            return;

        currentTime = timeFrame;
        currentPoints = 0;
    }

    public override void UpdatePillar()
    {
        currentTime -= Time.deltaTime;

        if (HasEnded)
        {
            myFirstClassManager.CloseAll();
        }
    }

    public void AddPoints(int points)
    {
        currentPoints += points;
    }
}
