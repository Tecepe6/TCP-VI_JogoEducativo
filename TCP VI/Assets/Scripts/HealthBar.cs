using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    // Variáveis para printar a vida
    public Combatant combatant;
    public TextMeshProUGUI textMeshPro;
    private BrandSO brandSO;
    
    // Define o valor máximo e o valor atual do slider como a vida máxima
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
        // Verifica se há uma atribuição de objeto com o Script Combatant
        // Se tiver, faz a variável brandSO deste script, receber os valores da instância da brandSO do objeto atribuído
        if(combatant != null)
        {
            brandSO = combatant._brandSO;
        }

        else
        {
            Debug.LogWarning("Combatente não atribuído");
        }

        if (textMeshPro != null && brandSO != null)
        {
            textMeshPro.text = $"{combatant.currentLife} / {brandSO.maxLife}";
        }

        else
        {
            Debug.LogWarning("TextMeshPro ou Brand não atribuído(s)");
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
