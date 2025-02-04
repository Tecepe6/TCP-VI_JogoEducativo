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
        // Define o valor de ambas as barras de vida e estamina para seu valor máximo (baseado nos atributos de cada mecha)
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

    // Corrotina. Função para esperar e executar uma ação após um tempo
    IEnumerator WaitAndExecute(float seconds, System.Action onComplete = null)
    {
        int randomNumber = Random.Range(1, 10);
        Debug.Log("Número Sorteado: " + randomNumber);

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
        // 30% de chance de não fazer nada e voltar para o estado Idle
        else
        {
            nextState = HelloWorldState.Idle;
        }

        Debug.Log("Esperando por " + seconds + " segundos...");
        yield return new WaitForSeconds(seconds);

        // Executa a ação após esperar
        onComplete?.Invoke();
    }

    public void HandleIdle()
    {
        // Inicia a corrotina para esperar antes de decidir o próximo movimento
        if(nextState != HelloWorldState.Null)
        {
            return;
        }

        // Faz a corrotina esperar o valor inserido na variável waitTime para então executar a troca de estados
        StartCoroutine(WaitAndExecute(waitTime, () =>
        {
            currentState = nextState;
            nextState = HelloWorldState.Null;
        }));
    }

    // Função chamada por eventos de animação que reseta os triggers de animação do Hello World. Servem para impedir que ele toque as animações de tomar dano imediatamente após sair do StrongPucnh
    public void ReturnToIdle()
    {
        currentState = HelloWorldState.Idle;
        animator.ResetTrigger("isTakingLightDamage");
        animator.ResetTrigger("isTakingHeavyDamage");
    }

    public override void QuickPunch()
    {
        // Verifica se a estamina atual é maior ou igual a estamina requerida para usar o QuickPunch
        if (currentStamina >= _brandSO.QuickPunchRequiredStamina)
        {
            Debug.Log("QUICK PUNCH!");

            // Subtrai a estamina requerida para usar o soco pela estamina atual
            currentStamina -= _brandSO.QuickPunchRequiredStamina;

            // Atualiza a barra de estamina com o valor atual
            staminaBar.SetStamina(currentStamina);

            // Chama a função que interrompe a corrotina de recuperação de estamina
            OnActionUsed();

            leftFist.QuickDamage();
            animator.SetTrigger("isQuickPunching");
        }
        else
            ReturnToIdle();
        
        // Implementar animação de falha
    }

    // Mesma lógica do QuickPunch, contudo, segue as informações de soco forte
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
        // Implementar animação de falha
    }

    // Mesma lógica de Quick Punch, mas com as informações de esquiva
    public override void DodgeLeft()
    {
        if(currentStamina >= _brandSO.DodgeRequiredStamina)
        {
            animator.SetTrigger("isLeftDodging");

            OnActionUsed();
            currentStamina -= _brandSO.DodgeRequiredStamina;
            staminaBar.SetStamina(currentStamina);

            // Espera pela duração da animação antes de voltar ao estado Idle
            StartCoroutine(WaitAndExecute(1f, () =>
            {
                // Transição para Idle após a animação
                currentState = HelloWorldState.Idle;
            }));
        }

        // Implementação de falha
    }

    // Mesma lógica de Quick Punch, mas com as informações de esquiva
    public override void DodgeRight()
    {
        if(currentStamina >= _brandSO.DodgeRequiredStamina)
        {
            animator.SetTrigger("isRightDodging");

            currentStamina -= _brandSO.DodgeRequiredStamina;
            staminaBar.SetStamina(currentStamina);
            OnActionUsed();

            // Espera pela duração da animação antes de voltar ao estado Idle
            StartCoroutine(WaitAndExecute(1f, () =>
            {
                // Transição para Idle após a animação
                currentState = HelloWorldState.Idle;
            }));
        }
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

        // Atualiza o valor da barra de vida para o valor da vida atual
        healthBar.SetHealth(currentLife);

        Debug.Log("Vida restante: " + currentLife);

        // Se o valor da vida atual for menor ou igual a 0, chama a função de derrotado da classe mãe Combatant
        if (currentLife <= 0)
        {
            Defeated();
        }
    }
}