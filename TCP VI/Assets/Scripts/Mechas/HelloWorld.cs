using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : Combatant
{
    Animator animator;
    [SerializeField] private float waitTime;

    public HelloWorldState nextState;

    // Estados do HelloWorld
    public enum HelloWorldState
    {
        Idle,
        QuickPunching,
        StrongPunching,
        LeftDodging,
        RightDodging,
        Null
    }

    [Header("ESTADO ATUAL")]
    public HelloWorldState currentState;

    void Start()
    {
        // Define o valor de ambas as barras de vida e estamina para seu valor m�ximo (baseado nos atributos de cada mecha)
        RestoreBars();

        // Pega o animator deste objeto
        animator = GetComponent<Animator>();

        // Define o estado atual do Hello World
        currentState = HelloWorldState.Idle;
        nextState = HelloWorldState.Null;
    }

    void Update()
    {
        // Troca de estados
        switch (currentState)
        {
            case HelloWorldState.Idle:
                HandleIdle();
                break;
            case HelloWorldState.QuickPunching:
                QuickPunch();
                break;
            case HelloWorldState.StrongPunching:
                StrongPunch();
                break;
            case HelloWorldState.LeftDodging:
                DodgeLeft();
                break;
            case HelloWorldState.RightDodging:
                DodgeRight();
                break;
        }
        
        StartStaminaRecovery();
    }

    // Corrotina. Fun��o para esperar e executar uma a��o ap�s um tempo
    IEnumerator WaitAndExecute(float seconds, System.Action onComplete = null)
    {
        int randomNumber = Random.Range(1, 10);
        Debug.Log("N�mero Sorteado: " + randomNumber);

        // 50% de chance de usar um quick punch
        
        if (randomNumber <= 5)
        {
            nextState = HelloWorldState.QuickPunching;
        }
        
        // 30% de chance de usar strong punch
        if (randomNumber  >= 7)
        {
            nextState = HelloWorldState.StrongPunching;
        }
        // 30% de chance de n�o fazer nada e voltar para o estado Idle
        else
        {
            nextState = HelloWorldState.Idle;
        }

        Debug.Log("Esperando por " + seconds + " segundos...");
        yield return new WaitForSeconds(seconds);

        // Executa a a��o ap�s esperar
        onComplete?.Invoke();
    }

    public void HandleIdle()
    {
        // Inicia a corrotina para esperar antes de decidir o pr�ximo movimento
        if(nextState != HelloWorldState.Null)
        {
            return;
        }

        // Faz a corrotina esperar o valor inserido na vari�vel waitTime para ent�o executar a troca de estados
        StartCoroutine(WaitAndExecute(waitTime, () =>
        {
            currentState = nextState;
            nextState = HelloWorldState.Null;
        }));
    }

    // Fun��o chamada por eventos de anima��o que reseta os triggers de anima��o do Hello World. Servem para impedir que ele toque as anima��es de tomar dano imediatamente ap�s sair do StrongPucnh
    public void ReturnToIdle()
    {
        currentState = HelloWorldState.Idle;
        animator.ResetTrigger("isTakingLightDamage");
        animator.ResetTrigger("isTakingHeavyDamage");
    }

    public override void QuickPunch()
    {
        // Verifica se a estamina atual � maior ou igual a estamina requerida para usar o QuickPunch
        if (currentStamina >= _brandSO.QuickPunchRequiredStamina)
        {
            Debug.Log("QUICK PUNCH!");

            // Subtrai a estamina requerida para usar o soco pela estamina atual
            currentStamina -= _brandSO.QuickPunchRequiredStamina;

            // Atualiza a barra de estamina com o valor atual
            staminaBar.SetStamina(currentStamina);

            // Chama a fun��o que interrompe a corrotina de recupera��o de estamina
            OnActionUsed();

            leftFist.QuickDamage();
            animator.SetTrigger("isQuickPunching");
        }
        else
            ReturnToIdle();
        
        // Implementar anima��o de falha
    }

    // Mesma l�gica do QuickPunch, contudo, segue as informa��es de soco forte
    public override void StrongPunch()
    {
        if(currentStamina >= _brandSO.StrongPunchRequiredStamina)
        {
            Debug.Log("STRONG PUNCH!!!");

            currentStamina -= _brandSO.StrongPunchRequiredStamina;
            staminaBar.SetStamina(currentStamina);
            OnActionUsed();

            rightFist.StrongDamage();
            animator.SetTrigger("isStrongPunching");           
        }
        else
            ReturnToIdle();
        // Implementar anima��o de falha
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
                currentState = HelloWorldState.Idle;
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
                currentState = HelloWorldState.Idle;
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