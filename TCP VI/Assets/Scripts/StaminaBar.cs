using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaBar : MonoBehaviour
{
    // Objeto que representa a barra
    public Slider slider;

    // Define de qual mecha reber� informa��es
    public Combatant combatant;

    // Pega o atributo de texto da interface (para atualizar o valor da barra
    public TextMeshProUGUI textMeshPro;

    // Pega a inst�ncia da marca
    private BrandSO brandSO;
    
    // Fun��o para atualizar o valor atual da barra para seu valor m�ximo
    public void SetMaxStamina(int stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
    }

    // Fun��o para atualizar o valor da barra da barra para o valor atual. � implementada pelas classes de mechas
    public void SetStamina(int stamina)
    {
        slider.value = stamina;
    }

    private void Start()
    {
        // Verifica se essa classe possui um mecha atribu�do � ela
        if(combatant != null)
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
        if(textMeshPro != null && brandSO != null)
        {
            // Define que o TMP deve pritnar o valor atual / valor m�ximo da barra
            textMeshPro.text = $"{combatant.currentStamina} / {brandSO.maxStamina}";
        }

        // Define uma mensagem de aviso, caso nenhum TMP esteja atribu�do
        else
        {
            Debug.LogWarning("Text Mesh Pro ou BrandSO n�o atribu�do(s)");
        }
    }

    private void Update()
    {
        // Atualiza o TMP para que os valores sejam atualizados
        textMeshPro.text = $"{combatant.currentStamina} / {brandSO.maxStamina}";
    }
}
