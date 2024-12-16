using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // TODO: Move this to another place
public abstract class Combatant : MonoBehaviour
{
    public int currentLife;
    public int currentStamina;

    private bool isRecoveringStamina = false;

    [Header("BARRAS")]
    public HealthBar healthBar;
    public StaminaBar staminaBar;

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
    public void RestoreBars()
    {
        // Barra de Vida
        currentLife = _brandSO.MaxLife;
        healthBar.SetMaxHealth(currentLife);

        // Barra de Stamina
        currentStamina = _brandSO.MaxStamina;
        staminaBar.SetMaxStamina(currentStamina);
        // Recuperação de stamina
        // Alteração: Agora recupera diretamente do _brandSO
        // Não é mais necessário armazenar em currentStaminaRecoveryRate.
    }

    // Funções de STAMINA
    public void OnActionUsed()
    {
        if (isRecoveringStamina)
        {
            StopCoroutine(RestoreStaminaOverTime());
            isRecoveringStamina = false;
        }
    }

    public void StartStaminaRecovery()
    {
        if (!isRecoveringStamina && currentStamina < _brandSO.MaxStamina) 
        {
            isRecoveringStamina = true;
            StartCoroutine(RestoreStaminaOverTime());
        }
    }

    private IEnumerator RestoreStaminaOverTime()
    {
        while (currentStamina < _brandSO.MaxStamina)
        {
            currentStamina += Mathf.RoundToInt(_brandSO.StaminaRecoveryRate * Time.deltaTime); // alterações
            currentStamina = Mathf.Clamp(currentStamina, 0, _brandSO.MaxStamina);
            staminaBar.SetStamina(currentStamina);

            yield return null;
        }

        isRecoveringStamina = false;
    }

    // Aplica dano ao Mecha
    public abstract void TakeDamage(int damageTaken, int tipoDeDano);

    // Lógica para a derrota do Mecha
    public void Defeated()
    {
        Debug.Log(gameObject.name + " foi derrotado!");
        Destroy(gameObject);
        SceneManager.LoadScene("CustomizationScene");
    }
}
