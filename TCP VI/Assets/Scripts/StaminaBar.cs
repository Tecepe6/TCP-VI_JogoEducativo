using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;

    public Combatant combatant;
    public TextMeshProUGUI textMeshPro;
    private BrandSO brandSO;
    
    public void SetMaxStamina(int stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
    }

    public void SetStamina(int stamina)
    {
        slider.value = stamina;
    }

    private void Start()
    {
        if(combatant != null)
        {
            brandSO = combatant._brandSO;
        }

        else
        {
            Debug.LogWarning("Combatente não atribuído");
        }

        if(textMeshPro != null && brandSO != null)
        {
            textMeshPro.text = $"{combatant.currentStamina} / {brandSO.maxStamina}";
        }

        else
        {
            Debug.LogWarning("Text Mesh Pro ou BrandSO não atribuído(s)");
        }
    }

    private void Update()
    {
        textMeshPro.text = $"{combatant.currentStamina} / {brandSO.maxStamina}";
    }
}
