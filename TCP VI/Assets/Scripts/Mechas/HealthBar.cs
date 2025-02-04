using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Objeto que representa a barra
    public Slider slider;

    // Define de qual mecha reber� informa��es
    public Combatant combatant;

    // Pega o atributo de texto da interface (para atualizar o valor da barra
    public TextMeshProUGUI textMeshPro;

    // Pega a inst�ncia da marca
    private BrandSO brandSO;
    
    // Define o valor m�ximo e o valor atual do slider como a vida m�xima
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // Fun��o para atualizar o valor da barra da barra para o valor atual. � implementada pelas classes de mechas
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    void Start()
    {
        // Verifica se essa classe possui um mecha atribu�do � ela
        if (combatant != null)
        {
            // Define a vari�vel brandSO com os valores do ScriptableObject associado ao mecha atribu�do
            brandSO = combatant._brandSO;
        }

        // Define uma mensagem de aviso, caso nenhum mecha esteja atribu�do
        else
        {
            Debug.LogWarning("Combatente n�o atribu�do");
        }

        // Verifica se � um TMP atribu�do
        if (textMeshPro != null && brandSO != null)
        {
            // Define que o TMP deve pritnar o valor atual / valor m�ximo da barra
            textMeshPro.text = $"{combatant.currentLife} / {brandSO.maxLife}";
        }

        // Define uma mensagem de aviso, caso nenhum TMP esteja atribu�do
        else
        {
            Debug.LogWarning("TextMeshPro ou Brand n�o atribu�do(s)");
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
