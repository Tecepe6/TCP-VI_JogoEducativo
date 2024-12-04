using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PartsContentList : MonoBehaviour
{
    [Header ("Prefab and ButtonsParent")]
    [SerializeField] GameObject buttonsPrefab;
    [SerializeField] Transform content;

    [SerializeField] List<GameObject> buttons;

    private void Awake() 
    {
        buttonsPrefab = Resources.Load<GameObject>("Prefabs/CustomizationUI/Parts_Btn");    
    }
    private void Start() 
    {
        MechaManager.instance.ChangingMenuToggled += CreateButtons;
        MechaManager.instance.ChangingMenuUntoggled += CleanButtons;
    }

    private void Update()
    {
        if(buttons.Count > 0)
        {
            int buttonID = MechaManager.instance.GetSelectedPartID;
            EventSystem.current.SetSelectedGameObject(buttons[buttonID]);
        }
        
    }

    private void CreateButtons(MechaManager.Selected bodyPart, 
                                List<ArmSO> leftArms, 
                                List<BrandSO> brands, 
                                List<ArmSO> rightArms)
    {
        if(bodyPart == MechaManager.Selected.RightArm)
        {
            foreach(ArmSO arm in rightArms)
            {
                GameObject newButton = Instantiate(buttonsPrefab, content);

                PartButton partButton = newButton.GetComponent<PartButton>();                
                partButton.SetButtonData(rightArms.IndexOf(arm), arm.Name);

                buttons.Add(newButton);
            }
        }
        if(bodyPart == MechaManager.Selected.Brand)
        {
            foreach(BrandSO brand in brands)
            {
                GameObject newButton = Instantiate(buttonsPrefab, content);

                PartButton partButton = newButton.GetComponent<PartButton>();                
                partButton.SetButtonData(brands.IndexOf(brand), brand.Name);

                buttons.Add(newButton);
            }
        }
        if(bodyPart == MechaManager.Selected.LeftArm)
        {
            foreach(ArmSO arm in leftArms)
            {
                GameObject newButton = Instantiate(buttonsPrefab, content);

                PartButton partButton = newButton.GetComponent<PartButton>();                
                partButton.SetButtonData(leftArms.IndexOf(arm), arm.Name);

                buttons.Add(newButton);
            }
        }
        EventSystem.current.SetSelectedGameObject(buttons[0]);
    }

    private void CleanButtons()
    {
        //Debug.Log("destroy all buttons!");
        foreach(GameObject button in buttons)
        {
            Destroy(button);
        }
        this.buttons.Clear();
    }
}
