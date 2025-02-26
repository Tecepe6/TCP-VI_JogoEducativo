using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class POOPillar : MonoBehaviour
{
    protected int currentPoints;
    [SerializeField] protected int pointsNeeded;

    [SerializeField] protected float timeFrame;
    protected float currentTime;

    public float CurrentTime => currentTime;

    private void Start()
    {
        currentTime = timeFrame;
        currentPoints = 0;
    }

    public abstract void UpdatePillar();

    public bool HasEnded => currentTime < 0;


    public bool HasWon => currentPoints >= pointsNeeded;
}
