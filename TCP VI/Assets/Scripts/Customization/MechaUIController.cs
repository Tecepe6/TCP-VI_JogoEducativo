using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MechaUIController : MonoBehaviour
{
    [SerializeField] GameObject buttonsPrefab;
    [SerializeField] Transform content;

    [Header ("UI Components")]
    [SerializeField] GameObject partsUI;
    [SerializeField] GameObject detailsUI;

    private void Awake() 
    {
        //getting UI components
        partsUI = getUIComponent("/Canvas/PartsUI");
        detailsUI = getUIComponent("/Canvas/DetailsUI");
    }

    private void Start() 
    {
        Instantiate(buttonsPrefab, content);
    }

    private void Update() 
    {
        UIVisibility();

        KeyboardControls();
    }

    private void CreateButtons(MechaManager.Selected bodyPart)
    {
        switch(bodyPart) 
        {
        case MechaManager.Selected.RightArm:
            Debug.Log("cheguei aqui");
            Instantiate(buttonsPrefab, content);
            break;
        case MechaManager.Selected.Brand:
            Instantiate(buttonsPrefab, content);
            break;
        case MechaManager.Selected.LeftArm:
            Instantiate(buttonsPrefab, content);
            break;
        }      
    }

    private GameObject getUIComponent(string componentName)
    {
        GameObject uiComponent = GameObject.Find(componentName);
        if(uiComponent == null)
        {
            Debug.LogError($"[DEV_ERROR] Cannot Find {componentName} object in scene.");
            return null;
        }
        else
        {
            return uiComponent;
        }
    }

    private void UIVisibility()
    {
        if(MechaManager.instance.GetChangingPart)
        {
            partsUI.SetActive(true);
            detailsUI.SetActive(true);
        }
        else
        {
            partsUI.SetActive(false);
            detailsUI.SetActive(false);
        }
    }
    
    private void KeyboardControls()
    {
        //Selecting Body Parts
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MechaManager.instance.SelectPreviousEnum();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MechaManager.instance.SelectNextEnum();
        }

        //Go back and forth the ChangingParts Menu
        if (Input.GetKeyDown(KeyCode.Return))
        {
            MechaManager.instance.ToggleChangingPart(true);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            MechaManager.instance.ToggleChangingPart(false);
            MechaManager.instance.ResetSelectedPartID();
        }

        //Go up and down our list of parts
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MechaManager.instance.SelectPreviousPartID();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MechaManager.instance.SelectNextPartID();
        }

        //Confirm an option
        if (MechaManager.instance.GetChangingPart && Input.GetKeyUp(KeyCode.Space))
        {
            MechaManager.instance.ConfirmChoice();
        }
    }

}
