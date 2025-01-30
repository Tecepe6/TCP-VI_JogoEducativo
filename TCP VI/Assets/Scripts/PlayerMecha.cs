using System;
using System.Collections;
using UnityEngine;

public class PlayerMecha : Combatant
{
    Animator animator;

    private bool isOnAnimation;

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
        // Define o valor de ambas as barras de vida e estamina para seu valor m�ximo (baseado nos atributos de cada mecha)
        RestoreBars();

        // Pega o animator deste objeto
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

    // Implementa a fun��o que interrompe a corrotina de recupera��o de estamina que � herdada da classe Combatant
    public override void OnActionUsed()
    {
        base.OnActionUsed();
    }

    // Implementa a fun��o que inicia a corrotina de recupera��o de estamina que � herdada da classe Combatant
    public override void StartStaminaRecovery()
    {
        base.StartStaminaRecovery();
    }

    // Implementa a corrotina de recupera��o de estamina que � herdada da classe Combatant
    protected override IEnumerator RecoverStamina()
    {
        yield return base.RecoverStamina();
    }

    // L�gica do soco r�pido. As outras fun��es, StrongPunch, DodgeRight e DodgeLeft seguem a mesma l�gica
    public override void QuickPunch()
    {
        // Verifica input do mouse e se a vari�vel isOnAnimation est� falsa antes de executar a l�gica do golpe
        if (Input.GetMouseButtonDown(0) && !isOnAnimation)
        {
            // Verifica se a estamina atual � maior ou igual a estamina necess�ria, de acordo com o ScriptableObject da marca
            if(currentStamina >= _brandSO.QuickPunchRequiredStamina)
            {
                // Define a booleana como verdadeira, impedindo que outras fun��es sejam utilizadas
                // Ela � redefinida como false dentro da fun��o ResetAnimationTriggers
                isOnAnimation = true;

                // Chama a fun��o QuickDamage do Fist para que aplique o dano
                leftFist.QuickDamage();

                // Ativa a anima��o do golpe
                animator.SetTrigger("isQuickPunching");

                // Subtrai a estamina atual para a estamina utilizada
                currentStamina -= _brandSO.QuickPunchRequiredStamina;

                // Atualiza a barra de estamina para o valor atual
                staminaBar.SetStamina(currentStamina);

                // Interrompe a corrotina de recupera��o de estamina
                OnActionUsed();
            }

            else
            {
                // Caso n�o tenha estamina o suficiente, toca a anima��o de estamina insuficiente
                animator.SetTrigger("insuficientStamina");
            }
        }
    }

    // Mesma l�gica do QuickPunch, s� que com as informa��es de ataque forte
    public override void StrongPunch()
    {
        if (Input.GetMouseButtonDown(1) && !isOnAnimation)
        {
            if(currentStamina >= _brandSO.StrongPunchRequiredStamina)
            {
                isOnAnimation = true;

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

    // Mesma l�gica de QuickPunch
    public override void DodgeLeft()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) && !isOnAnimation)
        {
            if(currentStamina >= _brandSO.DodgeRequiredStamina)
            {
                isOnAnimation = true;
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

    // Mesma l�gica de QuickPunch
    public override void DodgeRight()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && !isOnAnimation)
        {
            if(currentStamina >= _brandSO.DodgeRequiredStamina)
            {
                isOnAnimation = true;
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

    // Fun��o que reseta os triggers de anima��o do mecha do jogador que � IMPLEMENTADO ATRAV�S DE EVENTO DE ANIMA��O, que � individual para cada anima��o. Estes eventos s�o implementados nos �ltimos frames
    // Dessa forma, o mecha n�o ir� guardar inputs que sejam detectados antes desta fun��o ser chamada pelo evento da anima��o
    // Contudo, inputs detectados ap�s o evento da anima��o ir�o ser armazenados e executados assim que poss�vel. Dessa forma, tem-se um input buffer
    public void ResetAnimationTriggers()
    {
        // Define a booleana como false, permitindo que outras a��es sejam realizadas
        isOnAnimation = false;

        animator.ResetTrigger("isQuickPunching");
        animator.ResetTrigger("isStrongPunching");
        animator.ResetTrigger("isRightDodging");
        animator.ResetTrigger("isLeftDodging");
    }

    public override void TakeDamage(int damageTaken, int tipoDeDano)
    {
        // Reduz a vida baseado no dano recebido
        currentLife -= damageTaken;

        // Difere as anima��es baseado no tipoDeDano recebido
        if (tipoDeDano == 1)
        {
            animator.SetTrigger("isTakingLightDamage");
        }

        else if (tipoDeDano == 2)
        {
            animator.SetTrigger("isTakingHeavyDamage");
        }

        // Atualiza barra de vida para valor atual
        healthBar.SetHealth(currentLife);

        Debug.Log("Vida restante: " + currentLife);

        // Se o ataque fez a vida atual chegar � 0 ou um n�mero menor, chama a fun��o Defeated, que encerra a luta
        if (currentLife <= 0)
        {
            Defeated();
        }
    }
}
