using System;
using System.Collections;
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


    public override void OnActionUsed()
    {
        base.OnActionUsed();
    }


    public override void StartStaminaRecovery()
    {
        base.StartStaminaRecovery();
    }


    protected override IEnumerator RecoverStamina()
    {
        yield return base.RecoverStamina();
    }


    public override void QuickPunch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(currentStamina >= _brandSO.QuickPunchRequiredStamina)
            {
                leftFist.QuickDamage();
                animator.SetTrigger("isQuickPunching");

                currentStamina -= _brandSO.QuickPunchRequiredStamina;
                staminaBar.SetStamina(currentStamina);
                OnActionUsed();
            }

            else
            {
                animator.SetTrigger("insuficientStamina");
            }
        }
    }

    public override void StrongPunch()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(currentStamina >= _brandSO.StrongPunchRequiredStamina)
            {
                rightFist.StrongDamage();
                animator.SetTrigger("isStrongPunching");

                currentStamina -= _brandSO.StrongPunchRequiredStamina;
                staminaBar.SetStamina(currentStamina);
                OnActionUsed();
            }

            else
            {
                animator.SetTrigger("insuficientStamina");
            }
        }
    }

    public override void DodgeLeft()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentStamina >= _brandSO.DodgeRequiredStamina)
            {
                animator.SetTrigger("isLeftDodging");

                currentStamina -= _brandSO.DodgeRequiredStamina;
                staminaBar.SetStamina(currentStamina);
                OnActionUsed();
            }

            else
            {
                animator.SetTrigger("insuficientStamina");
            }
        }
    }

    public override void DodgeRight()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentStamina >= _brandSO.DodgeRequiredStamina)
            {
                animator.SetTrigger("isRightDodging");

                currentStamina -= _brandSO.DodgeRequiredStamina;
                staminaBar.SetStamina(currentStamina);
                OnActionUsed();
            
                
            }
            else
            {
                animator.SetTrigger("insuficientStamina");
            }

        
        }
    }


    public void ResetAnimationTriggers()
    {
        animator.ResetTrigger("isQuickPunching");
        animator.ResetTrigger("isStrongPunching");
        animator.ResetTrigger("isRightDodging");
        animator.ResetTrigger("isLeftDodging");
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
