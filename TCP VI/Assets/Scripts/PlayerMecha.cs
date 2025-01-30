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
        // Define o valor de ambas as barras de vida e estamina para seu valor máximo (baseado nos atributos de cada mecha)
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

    // Implementa a função que interrompe a corrotina de recuperação de estamina que é herdada da classe Combatant
    public override void OnActionUsed()
    {
        base.OnActionUsed();
    }

    // Implementa a função que inicia a corrotina de recuperação de estamina que é herdada da classe Combatant
    public override void StartStaminaRecovery()
    {
        base.StartStaminaRecovery();
    }

    // Implementa a corrotina de recuperação de estamina que é herdada da classe Combatant
    protected override IEnumerator RecoverStamina()
    {
        yield return base.RecoverStamina();
    }

    // Lógica do soco rápido. As outras funções, StrongPunch, DodgeRight e DodgeLeft seguem a mesma lógica
    public override void QuickPunch()
    {
        // Verifica input do mouse e se a variável isOnAnimation está falsa antes de executar a lógica do golpe
        if (Input.GetMouseButtonDown(0) && !isOnAnimation)
        {
            // Verifica se a estamina atual é maior ou igual a estamina necessária, de acordo com o ScriptableObject da marca
            if(currentStamina >= _brandSO.QuickPunchRequiredStamina)
            {
                // Define a booleana como verdadeira, impedindo que outras funções sejam utilizadas
                // Ela é redefinida como false dentro da função ResetAnimationTriggers
                isOnAnimation = true;

                // Chama a função QuickDamage do Fist para que aplique o dano
                leftFist.QuickDamage();

                // Ativa a animação do golpe
                animator.SetTrigger("isQuickPunching");

                // Subtrai a estamina atual para a estamina utilizada
                currentStamina -= _brandSO.QuickPunchRequiredStamina;

                // Atualiza a barra de estamina para o valor atual
                staminaBar.SetStamina(currentStamina);

                // Interrompe a corrotina de recuperação de estamina
                OnActionUsed();
            }

            else
            {
                // Caso não tenha estamina o suficiente, toca a animação de estamina insuficiente
                animator.SetTrigger("insuficientStamina");
            }
        }
    }

    // Mesma lógica do QuickPunch, só que com as informações de ataque forte
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

    // Mesma lógica de QuickPunch
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

    // Mesma lógica de QuickPunch
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

    // Função que reseta os triggers de animação do mecha do jogador que É IMPLEMENTADO ATRAVÉS DE EVENTO DE ANIMAÇÃO, que é individual para cada animação. Estes eventos são implementados nos últimos frames
    // Dessa forma, o mecha não irá guardar inputs que sejam detectados antes desta função ser chamada pelo evento da animação
    // Contudo, inputs detectados após o evento da animação irão ser armazenados e executados assim que possível. Dessa forma, tem-se um input buffer
    public void ResetAnimationTriggers()
    {
        // Define a booleana como false, permitindo que outras ações sejam realizadas
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

        // Difere as animações baseado no tipoDeDano recebido
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

        // Se o ataque fez a vida atual chegar à 0 ou um número menor, chama a função Defeated, que encerra a luta
        if (currentLife <= 0)
        {
            Defeated();
        }
    }
}
