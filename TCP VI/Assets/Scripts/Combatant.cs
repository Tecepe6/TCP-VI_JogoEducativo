using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public abstract class Combatant : MonoBehaviour
{
    public int currentLife;
    public int currentStamina;

    [SerializeField] protected bool isRecoveringStamina = false;

    [Header("BARRAS")]
    public HealthBar healthBar;
    public StaminaBar staminaBar;

    protected Coroutine staminaRecoveryCoroutine;

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

    // /*

    public virtual void OnActionUsed()
    {
        if (isRecoveringStamina)
        {
            Debug.Log("OnActionUsed()");
            StopCoroutine(staminaRecoveryCoroutine);
            isRecoveringStamina = false;
        }
    }

    public virtual void StartStaminaRecovery()
    {
        if (!isRecoveringStamina && currentStamina < _brandSO.MaxStamina)
        {
            Debug.Log("StartStaminaRecovery()");
            isRecoveringStamina = true;

            staminaRecoveryCoroutine = StartCoroutine(RecoverStamina());
        }
    }

    protected virtual IEnumerator RecoverStamina()
    {
        Debug.Log("RecoverStamina()");
        while (currentStamina < _brandSO.MaxStamina)
        {
            currentStamina += Convert.ToInt32(_brandSO.StaminaRecoveryRate * Time.deltaTime);

            if (currentStamina >= _brandSO.MaxStamina)
            {
                currentStamina = _brandSO.MaxStamina;
            }

            staminaBar.SetStamina(currentStamina);

            yield return null;
        }
    }

    // */

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
