using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PartDescription : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descriptionTMP;

    void Awake()
    {
        descriptionTMP = this.GetComponent<TextMeshProUGUI>();
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
        switch (bodyPart)
        {
            case MechaManager.Selected.RightArm:
                ID = MechaManager.instance.GetSelectedPartID;
                descriptionTMP.text = $"{rightArms[ID].Description}\n\n";
                break;
            case MechaManager.Selected.Brand:
                ID = MechaManager.instance.GetSelectedPartID;
                descriptionTMP.text = $"{brands[ID].Description}\n\n";
                break;
            case MechaManager.Selected.LeftArm:
                ID = MechaManager.instance.GetSelectedPartID;
                descriptionTMP.text = $"{leftArms[ID].Description}\n\n";
                break;
        }
    }
}
