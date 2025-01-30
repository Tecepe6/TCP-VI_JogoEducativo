using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public abstract class Combatant : MonoBehaviour
{
    [Header("STATUS")]
    public int currentLife;
    public int currentStamina;

    [SerializeField] protected bool isRecoveringStamina = false;

    [Header("BARRAS")]
    public HealthBar healthBar;
    public StaminaBar staminaBar;

    protected Coroutine recoverStaminaCoroutine;
    protected float staminaRecoveryAccumulated = 0f;

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
    }

    // Funções de STAMINA

    // É implementada nos scripts dos outros mechas para ser ativada quando estes usarem uma ação, parando a corrotina de recuperação de estamina
    public virtual void OnActionUsed()
    {
        if (isRecoveringStamina)
        {
            Debug.Log("OnActionUsed()");
            StopCoroutine(recoverStaminaCoroutine);
            isRecoveringStamina = false;

            // Impede que a estamina receba um valor negativo
            if(currentStamina < 0)
            {
                currentStamina = 0;
            }
        }
    }

    // Função que inicia a corrotina de recuperação de estamina
    public virtual void StartStaminaRecovery()
    {
        if (!isRecoveringStamina && currentStamina < _brandSO.MaxStamina)
        {
            Debug.Log("StartStaminaRecovery()");
            isRecoveringStamina = true;

            recoverStaminaCoroutine = StartCoroutine(RecoverStamina());
        }
    }

    // Corrotina de recuperação de estamina. Aumenta a estamina atual gradualmente, até seu valor máximo. É interrompida pela função OnActionUsed()
    protected virtual IEnumerator RecoverStamina()
    {
        Debug.Log("Corrotina de recuperação de estamina iniciada");
        yield return new WaitForSeconds(_brandSO.StaminaRegenDelay);

        while (currentStamina < _brandSO.MaxStamina)
        {
            staminaRecoveryAccumulated += (_brandSO.StaminaRecoveryRate) * Time.deltaTime;

            if (staminaRecoveryAccumulated >= 1f)
            {
                int increment = Mathf.FloorToInt(staminaRecoveryAccumulated);
                currentStamina += increment;
                staminaRecoveryAccumulated -= increment;

                currentStamina = Mathf.Min(currentStamina, _brandSO.MaxStamina);
                staminaBar.SetStamina(currentStamina);
            }

            yield return null;
        }

        yield return null;
    }

    // Aplica dano ao Mecha
    public abstract void TakeDamage(int damageTaken, int tipoDeDano);

    // Lógica para a derrota do Mecha
    public void Defeated()
    {
        Debug.Log(gameObject.name + " foi derrotado!");
        Destroy(gameObject);

        // Transiciona da tela atual para a cena de customização
        SceneManager.LoadScene("CustomizationScene");
    }
}