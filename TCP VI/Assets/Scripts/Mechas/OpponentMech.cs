using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentMech : Combatant
{
    [SerializeField] Animator animator;
    [Header("CONFIGURÁVEIS")]
    [SerializeField] private float waitTime;

    
    [SerializeField] private int chanceAtaqueRapido;
    [SerializeField] private int chanceAtaqueForte;

    [SerializeField] private int specialPunchCounter;
    [SerializeField] private int requiredSpecialPunchPoints;
    /*
    [SerializeField] private int chanceAtaqueEspecial;
    [SerializeField] private int chanceEsquivaDireita;
    [SerializeField] private int chanceEsquivaEsquerda;
    */

    public OpponentMechState nextState;

    // Estados do OpponentMech
    public enum OpponentMechState
    {
        Idle,
        QuickPunching,
        StrongPunching,
        SpecialPunching,
        LeftDodging,
        RightDodging,
        Null
    }

    [Header("ESTADO ATUAL")]
    public OpponentMechState currentState;

    void Start()
    {
        // Define o valor de ambas as barras de vida e estamina para seu valor m�ximo (baseado nos atributos de cada mecha)
        RestoreBars();

        // Pega o animator deste objeto
        animator = GetComponent<Animator>();

        // Define o estado atual do Hello World
        currentState = OpponentMechState.Idle;
        nextState = OpponentMechState.Null;
    }

    void Update()
    {
        // Troca de estados
        switch (currentState)
        {
            case OpponentMechState.Idle:
                HandleIdle();
                break;
            case OpponentMechState.QuickPunching:
                QuickPunch();
                break;
            case OpponentMechState.StrongPunching:
                StrongPunch();
                break;
            case OpponentMechState.SpecialPunching:
                SpecialPunch();
                break;
            case OpponentMechState.LeftDodging:
                DodgeLeft();
                break;
            case OpponentMechState.RightDodging:
                DodgeRight();
                break;
        }
        
        StartStaminaRecovery();
    }

    // Corrotina. Fun��o para esperar e executar uma a��o ap�s um tempo
    IEnumerator WaitAndExecute(float seconds, System.Action onComplete = null)
    {
        int randomNumber = Random.Range(1, 10);
        Debug.Log("Número Sorteado: " + randomNumber);

        // Resetando nextState antes de calcular a chance
        nextState = OpponentMechState.Idle;

        // Se tiver o número certo de special points, usa o ataque especial
        if(specialPunchCounter == requiredSpecialPunchPoints)
        {
            nextState = OpponentMechState.SpecialPunching;
        }

        // 50% de chance de usar um quick punch
        else if (randomNumber <= chanceAtaqueRapido)
        {
            specialPunchCounter++;

            nextState = OpponentMechState.QuickPunching;
        }
        // 40% de chance de usar strong punch
        else if (randomNumber <= chanceAtaqueForte)
        {
            specialPunchCounter++;

            nextState = OpponentMechState.StrongPunching;
        }

        Debug.Log("Esperando por " + seconds + " segundos...");
        yield return new WaitForSeconds(seconds);

        // Executa a ação após esperar
        onComplete?.Invoke();
    }

    public void HandleIdle()
    {
        // Inicia a corrotina para esperar antes de decidir o pr�ximo movimento
        if(nextState != OpponentMechState.Null)
        {
            return;
        }

        // Faz a corrotina esperar o valor inserido na vari�vel waitTime para ent�o executar a troca de estados
        StartCoroutine(WaitAndExecute(waitTime, () =>
        {
            currentState = nextState;
            nextState = OpponentMechState.Null;
        }));
    }

    // Fun��o chamada por eventos de anima��o que reseta os triggers de anima��o do Hello World. Servem para impedir que ele toque as anima��es de tomar dano imediatamente ap�s sair do StrongPucnh
    public void ReturnToIdle()
    {
        currentState = OpponentMechState.Idle;
        animator.ResetTrigger("isTakingLightDamage");
        animator.ResetTrigger("isTakingHeavyDamage");
    }

    public override void QuickPunch()
    {
        // Verifica se a estamina atual � maior ou igual a estamina requerida para usar o QuickPunch
        if (currentStamina >= _leftArmSO.QuickPunchRequiredStamina)
        {
            Debug.Log("QUICK PUNCH!");

            // Subtrai a estamina requerida para usar o soco pela estamina atual
            currentStamina -= _leftArmSO.QuickPunchRequiredStamina;

            // Atualiza a barra de estamina com o valor atual
            staminaBar.SetStamina(currentStamina);

            // Chama a fun��o que interrompe a corrotina de recupera��o de estamina
            OnActionUsed();

            leftFist.QuickDamage();
            animator.SetTrigger("isQuickPunching");

            // specialPunchCounter++;

            currentState = OpponentMechState.Idle;  
        }
        else
        {
            ReturnToIdle();
        }
        
        // Implementar anima��o de falha
    }

    // Mesma l�gica do QuickPunch, contudo, segue as informa��es de soco forte
    public override void StrongPunch()
    {
        if(currentStamina >= _rightArmSO.StrongPunchRequiredStamina)
        {
            Debug.Log("STRONG PUNCH!!!");

            currentStamina -= _rightArmSO.StrongPunchRequiredStamina;
            staminaBar.SetStamina(currentStamina);
            OnActionUsed();

            rightFist.StrongDamage();
            animator.SetTrigger("isStrongPunching");

            // specialPunchCounter++;

            currentState = OpponentMechState.Idle;
        }
        else
        {
            ReturnToIdle();
        }
        
        // Implementar anima��o de falha
    }

    public void SpecialPunch()
    {
        if(currentStamina >= _rightArmSO.SpecialPunchRequiredStamina)
        {
            Debug.Log("SPECIAL PUNCH!!!");

            currentStamina -= _rightArmSO.SpecialPunchRequiredStamina;
            staminaBar.SetStamina(currentStamina);
            OnActionUsed();

            rightFist.SpecialDamage();
            animator.SetTrigger("isSpecialPunching");

            specialPunchCounter = 0;

            currentState = OpponentMechState.Idle;
        }
    }

    // Mesma l�gica de Quick Punch, mas com as informa��es de esquiva
    public override void DodgeLeft()
    {
        if(currentStamina >= _brandSO.DodgeRequiredStamina)
        {
            animator.SetTrigger("isLeftDodging");

            OnActionUsed();
            currentStamina -= _brandSO.DodgeRequiredStamina;
            staminaBar.SetStamina(currentStamina);

            // Espera pela dura��o da anima��o antes de voltar ao estado Idle
            StartCoroutine(WaitAndExecute(1f, () =>
            {
                // Transi��o para Idle ap�s a anima��o
                currentState = OpponentMechState.Idle;
            }));
        }

        // Implementa��o de falha
    }

    // Mesma l�gica de Quick Punch, mas com as informa��es de esquiva
    public override void DodgeRight()
    {
        if(currentStamina >= _brandSO.DodgeRequiredStamina)
        {
            animator.SetTrigger("isRightDodging");

            currentStamina -= _brandSO.DodgeRequiredStamina;
            staminaBar.SetStamina(currentStamina);
            OnActionUsed();

            // Espera pela dura��o da anima��o antes de voltar ao estado Idle
            StartCoroutine(WaitAndExecute(1f, () =>
            {
                // Transi��o para Idle ap�s a anima��o
                currentState = OpponentMechState.Idle;
            }));
        }
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

        // Atualiza o valor da barra de vida para o valor da vida atual
        healthBar.SetHealth(currentLife);

        Debug.Log("Vida restante: " + currentLife);

        // Se o valor da vida atual for menor ou igual a 0, chama a fun��o de derrotado da classe m�e Combatant
        if (currentLife <= 0)
        {
            Defeated();
        }
    }
}