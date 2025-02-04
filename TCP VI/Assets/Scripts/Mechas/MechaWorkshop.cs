using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaWorkshop : Combatant
{
    // Recebe uma inst�ncia do animator
    Animator animator;

    // Define o tempo em que o mecha ir� esperar, no estado de idle, antes de tomar uma nova a��o
    [SerializeField] float tempoEspera;

    // Define quais ser�o os estados deste mecha
    public enum MechaEstado
    {
        Idle,
        SocoRapido,
        SocoForte,
        AtaqueEspecial,
        EsquivaEsquerda,
        EsquivaDireita
    }

    // Mostra no inspetor o estado atual do mecha
    [Header("ESTADO ATUAL")]
    public MechaEstado estadoAtual;

    void Start()
    {
        // Define os valores atuais das barras de vida e estamina para seus valores m�ximos
        RestoreBars();

        // Pega o componente animator deste objeto
        animator = GetComponent<Animator>();

        // Define o estado atual do mecha para idle
        estadoAtual = MechaEstado.Idle;
    }

    void Update()
    {
        // O switch � respons�vel por fazer a troca entre estados. Sendo um case para cada estado.
        switch (estadoAtual)
        {
            case MechaEstado.Idle:
                HandleIdle();
                break;
            case MechaEstado.SocoRapido:
                HandleSocoRapido();
                break;
            case MechaEstado.SocoForte:
                HandleSocoForte();
                break;
            case MechaEstado.AtaqueEspecial:
                HandleAtaqueEspecial();
                break;
            case MechaEstado.EsquivaEsquerda:
                HandleEsquivaEsquerda();
                break;
            case MechaEstado.EsquivaDireita:
                HandleEsquivaDireita();
                break;
        }

        // Come�a a corrotina de recupera��o de estamina
        StartStaminaRecovery();
    }

    // FUN��ES QUE LIDAM COM CADA ESTADO
    private void HandleIdle()
    {

    }

    private void HandleSocoRapido()
    {

    }

    public void HandleSocoForte()
    {

    }

    public void HandleAtaqueEspecial()
    {

    }

    public void HandleEsquivaEsquerda()
    {

    }

    public void HandleEsquivaDireita()
    {

    }

    // � mesmo necess�rio no Python?
    public void ReturnToIdle()
    {

    }

    // A��ES DO MECHA

    public override void QuickPunch()
    {
        // Verifica se a estamina atual � maior ou igual � estamina necess�ria para o QuickPunch
        if(currentStamina >= _brandSO.QuickPunchRequiredStamina)
        {
            currentStamina -= _brandSO.QuickPunchRequiredStamina;
            staminaBar.SetStamina(currentStamina);

            // Chama a fun�ao que interrompe, momentaneamente a corrotina de recupera��o de estamina
            OnActionUsed();

            // Aplica dano r�pido do bra�o esquerdo
            leftFist.QuickDamage();

            // Aplica o trigger que ir� tocar a anima��o
            animator.SetTrigger("usouSocoRapido");
        }
        else
        {
            animator.SetTrigger("usouSemEstamina");
        }
    }

    public override void StrongPunch()
    {
        if(currentStamina >= _brandSO.StrongPunchRequiredStamina)
        {
            currentStamina -= _brandSO.StrongPunchRequiredStamina;
            staminaBar.SetStamina(currentStamina);

            OnActionUsed();

            rightFist.StrongDamage();
            animator.SetTrigger("usouSocoForte");
        }
        else
        {
            animator.SetTrigger("usouSemEstamina");
        }
    }

    public void SpecialAttack()
    {
        if(currentStamina >= _leftArmSO.SpecialRequiredStamina)
        {
            currentStamina -= _leftArmSO.SpecialRequiredStamina;
            staminaBar.SetStamina(currentStamina);

            OnActionUsed();

            leftFist.SpecialDamage();
            animator.SetTrigger("usouAtaqueEspecial");
        }
        else
        {
            animator.SetTrigger("usouSemEstamina");
        }
    }

    public override void DodgeLeft()
    {
        if (currentStamina >= _brandSO.DodgeRequiredStamina)
        {
            currentStamina -= _brandSO.DodgeRequiredStamina;
            staminaBar.SetStamina(currentStamina);

            OnActionUsed();
            animator.SetTrigger("usouEsquivaEsquerda");
        }
        else
        {
            animator.SetTrigger("usouSemEstamina");
        }
    }

    public override void DodgeRight()
    {
        if (currentStamina >= _brandSO.DodgeRequiredStamina)
        {
            currentStamina -= _brandSO.DodgeRequiredStamina;
            staminaBar.SetStamina(currentStamina);

            OnActionUsed();
            animator.SetTrigger("usouEsquivaDireita");
        }
        else
        {
            animator.SetTrigger("usouSemEstamina");
        }
    }

    public override void TakeDamage(int damageTaken, int tipoDeDano)
    {
        // Reduz a vida baseado no dano recebido
        currentLife -= damageTaken;

        // Difere as anima��es baseado no tipoDeDano recebido
        if (tipoDeDano == 1)
        {
            animator.SetTrigger("usouTomarDanoFraco");
        }

        else if (tipoDeDano == 2)
        {
            animator.SetTrigger("usouTomarDanoForte");
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
