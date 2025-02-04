using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Python : Combatant
{
    Animator animator;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void QuickPunch()
    {
        throw new System.NotImplementedException();
    }

    public override void StrongPunch()
    {
        throw new System.NotImplementedException();
    }

    public override void DodgeLeft()
    {
        throw new System.NotImplementedException();
    }

    public override void DodgeRight()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int damageTaken, int tipoDeDano)
    {
        if(tipoDeDano == 1)
        {

        }
    }
}
