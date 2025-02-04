using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaWorkshop : Combatant
{
    // Recebe uma instância do animator
    Animator animator;

    // Define o tempo em que o mecha irá esperar, no estado de idle, antes de tomar uma nova ação
    [SerializeField] float tempoEspera;

    // Define quais serão os estados deste mecha
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
        // Define os valores atuais das barras de vida e estamina para seus valores máximos
        RestoreBars();

        // Pega o componente animator deste objeto
        animator = GetComponent<Animator>();

        // Define o estado atual do mecha para idle
        estadoAtual = MechaEstado.Idle;
    }

    void Update()
    {
        // O switch é responsável por fazer a troca entre estados. Sendo um case para cada estado.
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

        // Começa a corrotina de recuperação de estamina
        StartStaminaRecovery();
    }

    // FUNÇÕES QUE LIDAM COM CADA ESTADO
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

    // É mesmo necessário no Python?
    public void ReturnToIdle()
    {

    }

    // AÇÕES DO MECHA

    public override void QuickPunch()
    {
        // Verifica se a estamina atual é maior ou igual à estamina necessária para o QuickPunch
        if(currentStamina >= _brandSO.QuickPunchRequiredStamina)
        {
            currentStamina -= _brandSO.QuickPunchRequiredStamina;
            staminaBar.SetStamina(currentStamina);

            // Chama a funçao que interrompe, momentaneamente a corrotina de recuperação de estamina
            OnActionUsed();

            // Aplica dano rápido do braço esquerdo
            leftFist.QuickDamage();

            // Aplica o trigger que irá tocar a animação
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
        if(currentStamina >= _leftArmSO.RequiredSpecialStamina)
        {
            currentStamina -= _leftArmSO.RequiredSpecialStamina;
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
        throw new System.NotImplementedException();
    }

    public override void DodgeRight()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int damageTaken, int tipoDeDano)
    {
        throw new System.NotImplementedException();
    }
}
