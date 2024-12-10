using System;
using UnityEngine;

public class PlayerMecha : Combatant
{
    Animator animator;

    void Awake()
    {
        if (MechaManager.instance != null)
        {
            MechaManager.instance.SetPlayerReference(this);
            MechaManager.instance.SetMechaChanges();
        }

    }

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
        if (Input.GetMouseButtonDown(0))
        {
            rightFist.QuickDamage();
            animator.SetTrigger("isQuickPunching");
        }
    }

    public override void StrongPunch()
    {
        if (Input.GetMouseButtonDown(1))
        {
            leftFist.StrongDamage();
            animator.SetTrigger("isStrongPunching");
        }

    }

    public override void DodgeLeft()
    {
        if (Input.GetKeyDown(KeyCode.A))
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

    public override void TakeDamage(int damageTaken)
    {
        currentLife -= damageTaken;
        animator.SetTrigger("isTakingDamage");

        Debug.Log("Vida restante: " + currentLife);

        if (currentLife <= 0)
        {
            Defeated();
        }
    }
}
