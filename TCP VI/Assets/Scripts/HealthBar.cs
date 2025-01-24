using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    // Vari�veis para printar a vida
    public Combatant combatant;
    public TextMeshProUGUI textMeshPro;
    private BrandSO brandSO;
    
    // Define o valor m�ximo e o valor atual do slider como a vida m�xima
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // Define o valor do slider como vida
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    void Start()
    {
        // Verifica se h� uma atribui��o de objeto com o Script Combatant
        // Se tiver, faz a vari�vel brandSO deste script, receber os valores da inst�ncia da brandSO do objeto atribu�do
        if(combatant != null)
        {
            brandSO = combatant._brandSO;
        }

        else
        {
            Debug.LogWarning("Combatente n�o atribu�do");
        }

        if (textMeshPro != null && brandSO != null)
        {
            textMeshPro.text = $"{combatant.currentLife} / {brandSO.maxLife}";
        }

        else
        {
            Debug.LogWarning("TextMeshPro ou Brand n�o atribu�do(s)");
        }
    }

    void Update()
    {
        if (textMeshPro != null && combatant != null && brandSO != null)
        {
            textMeshPro.text = $"{combatant.currentLife} / {brandSO.maxLife}";
        }
    }
}
