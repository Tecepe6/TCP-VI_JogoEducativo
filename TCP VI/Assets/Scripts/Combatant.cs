using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaintsField;

public abstract class Combatant : MonoBehaviour
{
    [SerializeField] int currentLife;

    [Header("PEÇAS DO MECHA")]
    [SerializeField] ArmSO _rightArmSO;
    [SerializeField] ArmSO _leftArmSO;
    [SerializeField] BrandSO _brandSO;

    // Funções de uso geral para os mechas
    public void WeakPunch()
    {

    }

    public void StrongPunch()
    {

    }

    public void RestoreHealth()
    {
        currentLife = _brandSO.MaxLife;
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
