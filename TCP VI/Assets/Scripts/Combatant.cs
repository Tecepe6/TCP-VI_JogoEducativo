using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    [SerializeField] int vidaAtual;

    // Bra�o atuais do mecha
    public GameObject rightArm;
    public GameObject leftArm;


    // Fun��es de uso geral para os mechas
    public void RightPunch()
    {

    }

    public void LeftPunch()
    {

    }

    public void DodgeRight()
    {

    }

    public void DodgeLeft()
    {

    }

    public void TakeDamage()
    {

    }

    public void Defeated()
    {

    }
}
