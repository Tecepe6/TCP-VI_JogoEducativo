using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerMecha : Combatant
{
    Animator animator;
    void Start()
    {
        RestoreHealth();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        QuickPunch();
        StrongPunch();
    }

    public new void QuickPunch()
    {
        base.QuickPunch();

        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isQuickPunching");
        }
    }

    public new void StrongPunch()
    {
        base.StrongPunch();

        if(Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("isStrongPunching");
        }

    }
}
