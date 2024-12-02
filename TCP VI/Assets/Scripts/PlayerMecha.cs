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

        DodgeLeft();
        DodgeRight();
    }

    public override void QuickPunch()
    {
        if(Input.GetMouseButtonDown(0))
        {
            rightFist.QuickDamage();
            animator.SetTrigger("isQuickPunching");
        }
    }

    public override void StrongPunch()
    {
        if(Input.GetMouseButtonDown(1))
        {
            leftFist.StrongDamage();
            animator.SetTrigger("isStrongPunching");
        }

    }

    public override void DodgeLeft()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("isLeftDodging");
        }
        
    }

    public override void DodgeRight()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("isRightDodging");
        }
    }
}
