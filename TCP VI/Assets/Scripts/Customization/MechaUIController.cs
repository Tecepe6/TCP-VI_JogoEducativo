using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MechaUIController : MonoBehaviour
{
    // Leo
    [SerializeField] DialogueManager dialogueManager;

    [Header ("UI Components")]
    [SerializeField] GameObject partsUI;
    [SerializeField] GameObject detailsUI;

    private void Awake() 
    {
        //getting UI components
        partsUI = getUIComponent("/CustomizationCanvas/PartsUI");
        detailsUI = getUIComponent("/CustomizationCanvas/DetailsUI");
    }

    private void Update()
    {
        UIVisibility();

        // Leo:
        if (dialogueManager.isInDialogue == false)
        {

            KeyboardControls();

            if (Input.GetKeyDown(KeyCode.C))
            {
                SceneManager.LoadScene("CombatScene");
            }
        }
        // Leo
        else
        {
            partsUI.SetActive(false);
            detailsUI.SetActive(false);
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
            MechaManager.instance.SelectPreviousBodyPart();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MechaManager.instance.SelectNextBodyPart();
        }

        //Go back and forth the ChangingParts Menu
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            MechaManager.instance.ToggleChangingPart(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
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

    private void MouseControls()
    {
        // Implementar lógica de inputs do mouse por colisão
    }
}
