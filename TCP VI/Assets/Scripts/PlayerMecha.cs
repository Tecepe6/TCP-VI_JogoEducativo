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
        RestoreBars();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        QuickPunch();
        StrongPunch();

        DodgeLeft();
        DodgeRight();

        StartStaminaRecovery();
    }

    public override void QuickPunch()
    {
        if (Input.GetMouseButtonDown(0) && currentStamina >= _brandSO.QuickPunchRequiredStamina)
        {
            leftFist.QuickDamage();
            animator.SetTrigger("isQuickPunching");

            currentStamina -= 5;
            staminaBar.SetStamina(currentStamina);
            OnActionUsed();
        }

        //Implementar animação de falha
    }

    public override void StrongPunch()
    {
        if (Input.GetMouseButtonDown(1) && currentStamina >= _brandSO.StrongPunchRequiredStamina)
        {
            rightFist.StrongDamage();
            animator.SetTrigger("isStrongPunching");

            currentStamina -= 10;
            staminaBar.SetStamina(currentStamina);
            OnActionUsed();
        }

    }

    public override void DodgeLeft()
    {
        if (Input.GetKeyDown(KeyCode.A) && currentStamina >= _brandSO.DodgeRequiredStamina)
        {
            animator.SetTrigger("isLeftDodging");

            currentStamina -= 5;
            staminaBar.SetStamina(currentStamina);
            OnActionUsed();
        }

    }

    public override void DodgeRight()
    {
        if (Input.GetKeyDown(KeyCode.D) && currentStamina >= _brandSO.DodgeRequiredStamina)
        {
            animator.SetTrigger("isRightDodging");

            currentStamina -= 5;
            staminaBar.SetStamina(currentStamina);
            OnActionUsed();
        }
    }

    public override void TakeDamage(int damageTaken, int tipoDeDano)
    {
        currentLife -= damageTaken;

        if (tipoDeDano == 1)
        {
            animator.SetTrigger("isTakingLightDamage");
        }

        else if (tipoDeDano == 2)
        {
            animator.SetTrigger("isTakingHeavyDamage");
        }

        healthBar.SetHealth(currentLife);

        Debug.Log("Vida restante: " + currentLife);

        if (currentLife <= 0)
        {
            Defeated();
        }
    }
}
