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

    [Header("COLLIDERS DOS BRA�OS")]
    [SerializeField] BoxCollider rightCollider;
    [SerializeField] BoxCollider leftCollider;

    public int damageTaken;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu pertence � layer "Mecha"
        if (other.gameObject.layer == LayerMask.NameToLayer("Mecha"))
        {
            // Verifica se o collider do bra�o direito causou a colis�o
            if (rightCollider.bounds.Intersects(other.bounds))
            {
                damageTaken = _rightArmSO.QuickDamage;
            }
            // Verifica se o collider do bra�o esquerdo causou a colis�o
            else if (leftCollider.bounds.Intersects(other.bounds))
            {
                damageTaken = _leftArmSO.QuickDamage;
            }

            // Aplica o dano ap�s identificar o bra�o que causou a colis�o
            TakeDamage();
        }
    }

    // Fun��es de uso geral para os mechas
    public void QuickPunch()
    {
        // L�gica para um soco r�pido
    }

    public void StrongPunch()
    {
        // L�gica para um soco forte
    }

    public void RestoreHealth()
    {
        currentLife = _brandSO.MaxLife;
    }

    public void DodgeRight()
    {
        // L�gica para desvio � direita
    }

    public void DodgeLeft()
    {
        // L�gica para desvio � esquerda
    }

    public void TakeDamage()
    {
        currentLife -= damageTaken;
        currentLife = Mathf.Clamp(currentLife, 0, _brandSO.MaxLife);  // Limita a vida entre 0 e o m�ximo

        if (currentLife <= 0)
        {
            Defeated();
        }
    }

    public void Defeated()
    {
        Debug.Log(gameObject.name + " foi derrotado!");
        // L�gica para o caso de derrota
    }
}
