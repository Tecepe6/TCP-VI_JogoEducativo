using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    [SerializeField] protected int currentLife;

    [Header("PEÇAS DO MECHA")]
    public ArmSO _rightArmSO;
    public ArmSO _leftArmSO;
    public BrandSO _brandSO;

    public Fist rightFist;
    public Fist leftFist;

    public int currentDamage;

    // Função abstrata para socos e esquivas
    public abstract void QuickPunch();
    public abstract void StrongPunch();
    public abstract void DodgeRight();
    public abstract void DodgeLeft();

    // Restaura a vida ao máximo
    public void RestoreHealth()
    {
        currentLife = _brandSO.MaxLife;
        Debug.Log(currentLife);
    }

    // Aplica dano ao Mecha
    public void TakeDamage(int damageTaken)
    {
        currentLife -= damageTaken;
        Debug.Log("Vida restante: " + currentLife);

        if (currentLife <= 0)
        {
            Defeated();
        }
    }

    // Lógica para a derrota do Mecha
    public void Defeated()
    {
        Debug.Log(gameObject.name + " foi derrotado!");
        Destroy(gameObject); // Exemplo: destrói o Mecha derrotado
    }
}
