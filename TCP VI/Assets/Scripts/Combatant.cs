using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // TODO: Move this to another place
public abstract class Combatant : MonoBehaviour
{
    [SerializeField] protected int currentLife;

    [Header("PE�AS DO MECHA")]
    public ArmSO _rightArmSO;
    public ArmSO _leftArmSO;
    public BrandSO _brandSO;

    public Fist rightFist;
    public Fist leftFist;

    public int currentDamage;

    // Fun��o abstrata para socos e esquivas
    public abstract void QuickPunch();
    public abstract void StrongPunch();
    public abstract void DodgeRight();
    public abstract void DodgeLeft();

    // Restaura a vida ao m�ximo
    public void RestoreHealth()
    {
        currentLife = _brandSO.MaxLife;
        Debug.Log(currentLife);
    }

    // Aplica dano ao Mecha
    public abstract void TakeDamage(int damageTaken);

    // L�gica para a derrota do Mecha
    public void Defeated()
    {
        Debug.Log(gameObject.name + " foi derrotado!");
        Destroy(gameObject); // Exemplo: destr�i o Mecha derrotado
        SceneManager.LoadScene("CustomizationScene");
    }
}
