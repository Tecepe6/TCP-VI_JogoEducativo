using System.Collections;
using UnityEngine;

public class TesteEstamina : MonoBehaviour
{
    public int currentStamina;
    public int maxStamina;
    [Range(5, 100)] public int staminaRecoveryRate;

    private float staminaRecoveryAccumulated = 0f;

    public StaminaBar staminaBar;

    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.SetStamina(currentStamina);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentStamina -= 50;
            staminaBar.SetStamina(currentStamina);
        }

        if (currentStamina < maxStamina)
        {
            StartCoroutine(RecoverStamina());
        }
    }

    protected IEnumerator RecoverStamina()
    {
        Debug.Log("Corrotina de recuperação de estamina iniciada");
        yield return new WaitForSeconds(1f);

        while (currentStamina < maxStamina)
        {
            staminaRecoveryAccumulated += (staminaRecoveryRate / 100f) * Time.deltaTime;

            if (staminaRecoveryAccumulated >= 1f)
            {
                int increment = Mathf.FloorToInt(staminaRecoveryAccumulated);
                currentStamina += increment;
                staminaRecoveryAccumulated -= increment;

                currentStamina = Mathf.Min(currentStamina, maxStamina);
                staminaBar.SetStamina(currentStamina);
            }

            yield return null;
        }

        yield return null;
    }
}