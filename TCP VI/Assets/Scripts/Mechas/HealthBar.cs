using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Objeto que representa a barra
    public Slider slider;

    // Define de qual mecha reberá informações
    public Combatant combatant;

    // Pega o atributo de texto da interface (para atualizar o valor da barra
    public TextMeshProUGUI textMeshPro;

    // Pega a instância da marca
    private BrandSO brandSO;
    
    // Define o valor máximo e o valor atual do slider como a vida máxima
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // Função para atualizar o valor da barra da barra para o valor atual. É implementada pelas classes de mechas
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    void Start()
    {
        // Verifica se essa classe possui um mecha atribuído à ela
        if (combatant != null)
        {
            // Define a variável brandSO com os valores do ScriptableObject associado ao mecha atribuído
            brandSO = combatant._brandSO;
        }

        // Define uma mensagem de aviso, caso nenhum mecha esteja atribuído
        else
        {
            Debug.LogWarning("Combatente não atribuído");
        }

        // Verifica se à um TMP atribuído
        if (textMeshPro != null && brandSO != null)
        {
            // Define que o TMP deve pritnar o valor atual / valor máximo da barra
            textMeshPro.text = $"{combatant.currentLife} / {brandSO.maxLife}";
        }

        // Define uma mensagem de aviso, caso nenhum TMP esteja atribuído
        else
        {
            Debug.LogWarning("TextMeshPro ou Brand não atribuído(s)");
        }
    }

    void Update()
    {
        // Atualiza o TMP para que os valores sejam atualizados
        if (textMeshPro != null && combatant != null && brandSO != null)
        {
            textMeshPro.text = $"{combatant.currentLife} / {brandSO.maxLife}";
        }
    }
}
