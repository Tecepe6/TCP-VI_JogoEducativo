using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    [SerializeField] int currentLife;

    [Header("PE�AS DO MECHA")]
    [SerializeField] ArmSO _rightArmSO;
    [SerializeField] ArmSO _leftArmSO;
    [SerializeField] BrandSO _brandSO;

    // Fun��es de uso geral para os mechas
    public void QuickPunch()
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
