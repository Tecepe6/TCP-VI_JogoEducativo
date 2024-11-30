using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : Combatant
{
    void Start()
    {
        RestoreHealth();
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
}
