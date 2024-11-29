using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant : MonoBehaviour
{
    [SerializeField] int currentLife;

    [Header("PEÇAS DO MECHA")]
    [SerializeField] ArmSO _rightArmSO;
    [SerializeField] ArmSO _leftArmSO;
    [SerializeField] BrandSO _brandSO;

    [Header("COLLIDERS DOS BRAÇOS")]
    [SerializeField] BoxCollider rightCollider;
    [SerializeField] BoxCollider leftCollider;

    public int damageTaken;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu pertence à layer "Mecha"
        if (other.gameObject.layer == LayerMask.NameToLayer("Mecha"))
        {
            // Verifica se o collider do braço direito causou a colisão
            if (rightCollider.bounds.Intersects(other.bounds))
            {
                damageTaken = _rightArmSO.QuickDamage;
            }
            // Verifica se o collider do braço esquerdo causou a colisão
            else if (leftCollider.bounds.Intersects(other.bounds))
            {
                damageTaken = _leftArmSO.QuickDamage;
            }

            // Aplica o dano após identificar o braço que causou a colisão
            TakeDamage();
        }
    }

    // Funções de uso geral para os mechas
    public void QuickPunch()
    {
        // Lógica para um soco rápido
    }

    public void StrongPunch()
    {
        // Lógica para um soco forte
    }

    public void RestoreHealth()
    {
        currentLife = _brandSO.MaxLife;
    }

    public void DodgeRight()
    {
        // Lógica para desvio à direita
    }

    public void DodgeLeft()
    {
        // Lógica para desvio à esquerda
    }

    public void TakeDamage()
    {
        currentLife -= damageTaken;
        currentLife = Mathf.Clamp(currentLife, 0, _brandSO.MaxLife);  // Limita a vida entre 0 e o máximo

        if (currentLife <= 0)
        {
            Defeated();
        }
    }

    public void Defeated()
    {
        Debug.Log(gameObject.name + " foi derrotado!");
        // Lógica para o caso de derrota
    }
}
