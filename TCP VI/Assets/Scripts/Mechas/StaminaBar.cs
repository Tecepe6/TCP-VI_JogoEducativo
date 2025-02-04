using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaBar : MonoBehaviour
{
    // Objeto que representa a barra
    public Slider slider;

    // Define de qual mecha reberá informações
    public Combatant combatant;

    // Pega o atributo de texto da interface (para atualizar o valor da barra
    public TextMeshProUGUI textMeshPro;

    // Pega a instância da marca
    private BrandSO brandSO;
    
    // Função para atualizar o valor atual da barra para seu valor máximo
    public void SetMaxStamina(int stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
    }

    // Função para atualizar o valor da barra da barra para o valor atual. É implementada pelas classes de mechas
    public void SetStamina(int stamina)
    {
        slider.value = stamina;
    }

    private void Start()
    {
        // Verifica se essa classe possui um mecha atribuído à ela
        if(combatant != null)
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
        if(textMeshPro != null && brandSO != null)
        {
            // Define que o TMP deve pritnar o valor atual / valor máximo da barra
            textMeshPro.text = $"{combatant.currentStamina} / {brandSO.maxStamina}";
        }

        // Define uma mensagem de aviso, caso nenhum TMP esteja atribuído
        else
        {
            Debug.LogWarning("Text Mesh Pro ou BrandSO não atribuído(s)");
        }
    }

    private void Update()
    {
        // Atualiza o TMP para que os valores sejam atualizados
        textMeshPro.text = $"{combatant.currentStamina} / {brandSO.maxStamina}";
    }
}
