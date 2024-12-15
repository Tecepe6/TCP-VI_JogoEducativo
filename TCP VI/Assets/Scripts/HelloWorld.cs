using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : Combatant
{
    Animator animator;

    // Estados do HelloWorld
    public enum HelloWorldState
    {
        Idle,
        QuickPunching,
        StrongPunching,
        LeftDodging,
        RightDodging
    }

    [Header("ESTADO ATUAL")]
    public HelloWorldState currentState;

    void Start()
    {
        RestoreHealth();
        animator = GetComponent<Animator>();

        currentState = HelloWorldState.Idle;
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
        Debug.Log("Esperando por " + seconds + " segundos...");
        yield return new WaitForSeconds(seconds);

        // Executa a a��o ap�s esperar
        onComplete?.Invoke();
    }

    public void HandleIdle()
    {
        // Inicia a corrotina para esperar antes de decidir o pr�ximo movimento
        StartCoroutine(WaitAndExecute(2f, () =>
        {
            int randomNumber = Random.Range(1, 10);
            Debug.Log("N�mero Sorteado: " + randomNumber);

            // 30% de chance de usar um quick punch
            if (randomNumber <= 3)
            {
                currentState = HelloWorldState.QuickPunching;
            }
            // 20% de chance de usar strong punch
            else if (randomNumber >= 9)
            {
                currentState = HelloWorldState.StrongPunching;
            }
            // 50% de chance de n�o fazer nada e voltar para o estado Idle
            else
            {
                currentState = HelloWorldState.Idle;
            }
        }));
    }

    public override void QuickPunch()
    {
        Debug.Log("QUICK PUNCH!");

        rightFist.QuickDamage();
        animator.SetTrigger("isQuickPunching");

        // Espera pela dura��o da anima��o antes de voltar ao estado Idle
        StartCoroutine(WaitAndExecute(1f, () =>
        {
            // Transi��o para Idle ap�s a anima��o
            currentState = HelloWorldState.Idle;
        }));
    }

    public override void StrongPunch()
    {
        Debug.Log("STRONG PUNCH!!!");

        leftFist.StrongDamage();
        animator.SetTrigger("isStrongPunching");

        // Espera pela dura��o da anima��o antes de voltar ao estado Idle
        StartCoroutine(WaitAndExecute(2f, () =>
        {
            // Transi��o para Idle ap�s a anima��o
            currentState = HelloWorldState.Idle;
        }));
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

    public override void TakeDamage(int damageTaken, string tipoDeDano)
    {
        currentLife -= damageTaken;

        if (tipoDeDano == "Dano Fraco")
        {
            animator.SetTrigger("isTakingLightDamage");
        }

        else if(tipoDeDano == "Dano Forte")
        {
            animator.SetTrigger("isTakingHeavyDamage");
        }
        

        Debug.Log("Vida restante: " + currentLife);

        if (healthManager != null)
        {
            healthManager.UpdateHealthBar();
        }
        else
        {
            Debug.LogWarning("HealthManager n�o est� configurado corretamente.");
        }

        if (currentLife <= 0)
        {
            Defeated();
        }
    }
}