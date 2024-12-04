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
                detailsTMP.text = rightArms[ID].Description;
                break;
            case MechaManager.Selected.Brand:
                ID = MechaManager.instance.GetSelectedPartID;
                detailsTMP.text = brands[ID].Description;
                break;
            case MechaManager.Selected.LeftArm:
                ID = MechaManager.instance.GetSelectedPartID;
                detailsTMP.text = leftArms[ID].Description;
                break;
        }
    }

}
