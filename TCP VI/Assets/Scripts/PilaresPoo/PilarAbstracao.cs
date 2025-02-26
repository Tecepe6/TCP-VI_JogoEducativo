using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarAbstracao : POOPillar
{
    [SerializeField] AbstButtonManager abstButtonManager;
    [SerializeField] MyFirstClassManager myFirstClassManager;

    

    public void Initialize()
    {

        if (HasWon)
            return;
        currentTime = timeFrame;
        currentPoints = 0;
        abstButtonManager.StartButtons();
    }

    public override void UpdatePillar()
    {
        currentTime -= Time.deltaTime;

        if(HasEnded)
        {
            myFirstClassManager.CloseAll();
        }
    }

    public void AddPoints(int points)
    {
        currentPoints += points;
    }
}
