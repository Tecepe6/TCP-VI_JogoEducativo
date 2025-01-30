using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Details : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI detailsTMP;

    void Awake()
    {
        detailsTMP = this.GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        MechaManager.instance.PartSelected += UpdateDetailsText;
    }

    public void UpdateDetailsText(MechaManager.Selected bodyPart, 
                                List<ArmSO> leftArms, 
                                List<BrandSO> brands, 
                                List<ArmSO> rightArms)
    {
        int ID;
        switch(bodyPart) 
        {
            case MechaManager.Selected.RightArm:
                ID = MechaManager.instance.GetSelectedPartID;
                detailsTMP.text = $"{rightArms[ID].Description}\n\n"+
                $"> Dano do Jab: {rightArms[ID].QuickDamage}\n" +
                $"> Dano do Direto: {rightArms[ID].StrongDamage}";
                break;
            case MechaManager.Selected.Brand:
                ID = MechaManager.instance.GetSelectedPartID;
                detailsTMP.text = $"{brands[ID].Description}\n\n"+
                $"> Vida Máx.: {brands[ID].MaxLife}\n" +
                $"> Estamina Máx.: {brands[ID].MaxStamina}\n" +
                $"> Deley de Recuperação de Estamina: {brands[ID].StaminaRegenDelay}\n"+
                $"> Custo Jab: {brands[ID].QuickPunchRequiredStamina}\n" +
                $"> Custo Direto: {brands[ID].StrongPunchRequiredStamina}\n" +
                $"> Custo Esquiva: { brands[ID].DodgeRequiredStamina}";
                break;
            case MechaManager.Selected.LeftArm:
                ID = MechaManager.instance.GetSelectedPartID;
                detailsTMP.text = $"{leftArms[ID].Description}\n\n"+
                $"> Dano do Jab: {leftArms[ID].QuickDamage}\n" +
                $"> Dano do Direto: {leftArms[ID].StrongDamage}";
                break;
        }
    }

}
