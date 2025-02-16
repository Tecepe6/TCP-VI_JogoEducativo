using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Python : Combatant
{
    Animator animator;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void QuickPunch()
    {
        throw new System.NotImplementedException();
    }

    public override void StrongPunch()
    {
        throw new System.NotImplementedException();
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
