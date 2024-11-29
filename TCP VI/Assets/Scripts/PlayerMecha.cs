using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        TakeDamage();

        DodgeLeft();
        DodgeRight();
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

    public new void DodgeLeft()
    {
        base.DodgeLeft();
        
        if(Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("isLeftDodging");
        }
        
    }

    public new void DodgeRight()
    {
        base.DodgeRight();

        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("isRightDodging");
        }
    }
}
