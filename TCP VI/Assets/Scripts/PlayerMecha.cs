using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerMecha : Combatant
{
    void Start()
    {
        RestoreHealth();
    }

    void Update()
    {
        WeakPunch();
    }

    public new void WeakPunch()
    {
        base.WeakPunch();

    }
}
