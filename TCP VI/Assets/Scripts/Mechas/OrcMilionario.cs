using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMilionario : Combatant
{
    [Header("BRAÇOS EXTRAS")]
    public ArmSO _armSO2;
    public ArmSO _armSO3;

    Animator animator;
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

    public OrcMilionarioState nextState;

    // Estados do OrcMilionario
    public enum OrcMilionarioState
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
    public OrcMilionarioState currentState;

    void Start()
    {
        // Define o valor de ambas as barras de vida e estamina para seu valor m�ximo (baseado nos atributos de cada mecha)
        RestoreBars();

        // Pega o animator deste objeto
        animator = GetComponent<Animator>();

        // Define o estado atual do Hello World
        currentState = OrcMilionarioState.Idle;
        nextState = OrcMilionarioState.Null;
    }

    void Update()
    {
        // Troca de estados
        switch (currentState)
        {
            case OrcMilionarioState.Idle:
                HandleIdle();
                break;
            case OrcMilionarioState.QuickPunching:
                QuickPunch();
                break;
            case OrcMilionarioState.StrongPunching:
                StrongPunch();
                break;
            case OrcMilionarioState.SpecialPunching:
                SpecialPunch();
                break;
            case OrcMilionarioState.LeftDodging:
                DodgeLeft();
                break;
            case OrcMilionarioState.RightDodging:
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
        nextState = OrcMilionarioState.Idle;

        // Se tiver o número certo de special points, usa o ataque especial
        if (specialPunchCounter == requiredSpecialPunchPoints)
        {
            nextState = OrcMilionarioState.SpecialPunching;
        }

        // 50% de chance de usar um quick punch
        else if (randomNumber <= chanceAtaqueRapido)
        {
            specialPunchCounter++;

            nextState = OrcMilionarioState.QuickPunching;
        }
        // 40% de chance de usar strong punch
        else if (randomNumber <= chanceAtaqueForte)
        {
            specialPunchCounter++;

            nextState = OrcMilionarioState.StrongPunching;
        }

        Debug.Log("Esperando por " + seconds + " segundos...");
        yield return new WaitForSeconds(seconds);

        // Executa a ação após esperar
        onComplete?.Invoke();
    }

    public void HandleIdle()
    {
        // Inicia a corrotina para esperar antes de decidir o pr�ximo movimento
        if (nextState != OrcMilionarioState.Null)
        {
            return;
        }

        // Faz a corrotina esperar o valor inserido na vari�vel waitTime para ent�o executar a troca de estados
        StartCoroutine(WaitAndExecute(waitTime, () =>
        {
            currentState = nextState;
            nextState = OrcMilionarioState.Null;
        }));
    }

    // Fun��o chamada por eventos de anima��o que reseta os triggers de anima��o do Hello World. Servem para impedir que ele toque as anima��es de tomar dano imediatamente ap�s sair do StrongPucnh
    public void ReturnToIdle()
    {
        currentState = OrcMilionarioState.Idle;
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

            // specialPunchCounter++;

            currentState = OrcMilionarioState.Idle;
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
        if (currentStamina >= _brandSO.StrongPunchRequiredStamina)
        {
            Debug.Log("STRONG PUNCH!!!");

            currentStamina -= _brandSO.StrongPunchRequiredStamina;
            staminaBar.SetStamina(currentStamina);
            OnActionUsed();

            rightFist.StrongDamage();
            animator.SetTrigger("isStrongPunching");

            // specialPunchCounter++;

            currentState = OrcMilionarioState.Idle;
        }
        else
        {
            ReturnToIdle();
        }

        // Implementar anima��o de falha
    }

    public void SpecialPunch()
    {
        if (currentStamina >= _brandSO.SpecialPunchRequiredStamina)
        {
            Debug.Log("SPECIAL PUNCH!!!");

            currentStamina -= _brandSO.SpecialPunchRequiredStamina;
            staminaBar.SetStamina(currentStamina);
            OnActionUsed();

            rightFist.SpecialDamage();
            animator.SetTrigger("isSpecialPunching");

            specialPunchCounter = 0;

            currentState = OrcMilionarioState.Idle;
        }
    }

    // Mesma l�gica de Quick Punch, mas com as informa��es de esquiva
    public override void DodgeLeft()
    {
        if (currentStamina >= _brandSO.DodgeRequiredStamina)
        {
            animator.SetTrigger("isLeftDodging");

            OnActionUsed();
            currentStamina -= _brandSO.DodgeRequiredStamina;
            staminaBar.SetStamina(currentStamina);

            // Espera pela dura��o da anima��o antes de voltar ao estado Idle
            StartCoroutine(WaitAndExecute(1f, () =>
            {
                // Transi��o para Idle ap�s a anima��o
                currentState = OrcMilionarioState.Idle;
            }));
        }

        // Implementa��o de falha
    }

    // Mesma l�gica de Quick Punch, mas com as informa��es de esquiva
    public override void DodgeRight()
    {
        if (currentStamina >= _brandSO.DodgeRequiredStamina)
        {
            animator.SetTrigger("isRightDodging");

            currentStamina -= _brandSO.DodgeRequiredStamina;
            staminaBar.SetStamina(currentStamina);
            OnActionUsed();

            // Espera pela dura��o da anima��o antes de voltar ao estado Idle
            StartCoroutine(WaitAndExecute(1f, () =>
            {
                // Transi��o para Idle ap�s a anima��o
                currentState = OrcMilionarioState.Idle;
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