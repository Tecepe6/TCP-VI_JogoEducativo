using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : Combatant
{
    Animator animator;

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
        RestoreHealth();
        animator = GetComponent<Animator>();

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
    }

    // Fun��o para esperar e executar uma a��o ap�s um tempo
    IEnumerator WaitAndExecute(float seconds, System.Action onComplete = null)
    {
        int randomNumber = Random.Range(1, 10);
        Debug.Log("N�mero Sorteado: " + randomNumber);

        // 30% de chance de usar um quick punch
        if (randomNumber <= 3)
        {
            nextState = HelloWorldState.QuickPunching;
        }
        // 20% de chance de usar strong punch
        else if (randomNumber >= 9)
        {
            nextState = HelloWorldState.StrongPunching;
        }
        // 50% de chance de n�o fazer nada e voltar para o estado Idle
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

        StartCoroutine(WaitAndExecute(2f, () =>
        {
            currentState = nextState;
            nextState = HelloWorldState.Null;
        }));
    }

    public override void QuickPunch()
    {
        Debug.Log("QUICK PUNCH!");

        leftFist.QuickDamage();
        animator.SetTrigger("isQuickPunching");

        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash("QuickAttack"))
        {
            return;
        }

        currentState = HelloWorldState.Idle;
    }

    public override void StrongPunch()
    {
        Debug.Log("STRONG PUNCH!!!");

        rightFist.StrongDamage();
        animator.SetTrigger("isStrongPunching");

        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash("StrongAttack"))
        {
            return;
        }

        currentState = HelloWorldState.Idle;
    }

    public override void DodgeLeft()
    {
        animator.SetTrigger("isLeftDodging");

        // Espera pela dura��o da anima��o antes de voltar ao estado Idle
        StartCoroutine(WaitAndExecute(1f, () =>
        {
            // Transi��o para Idle ap�s a anima��o
            currentState = HelloWorldState.Idle;
        }));
    }

    public override void DodgeRight()
    {
        animator.SetTrigger("isRightDodging");

        // Espera pela dura��o da anima��o antes de voltar ao estado Idle
        StartCoroutine(WaitAndExecute(1f, () =>
        {
            // Transi��o para Idle ap�s a anima��o
            currentState = HelloWorldState.Idle;
        }));
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