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

    //Juaun
    [SerializeField] GameObject notebookUI;

    //Pedro
    Camera cam;
    [SerializeField] LayerMask layerMask;

    private void Awake() 
    {
        //getting UI components
        partsUI = getUIComponent("/CustomizationCanvas/Tela de Customiza��o/PartsUI");
        detailsUI = getUIComponent("/CustomizationCanvas/Tela de Customiza��o/DetailsUI");

        notebookUI.SetActive(false);

        cam = Camera.main;
    }

    private void Update()
    {
        UIVisibility();

        // Leo:
        if (!dialogueManager.isInDialogue)
        {

            KeyboardControls();
            MouseControls();

            if (Input.GetKeyDown(KeyCode.C))
            {
                // Old method: SceneManager.LoadScene("Apresentacao");
                // Load the current scene immediatly: DaysManager.instance.LoadCurrentDay();
                
                // Load the Day Selection Screen:
                AsyncOperation operation = SceneManager.LoadSceneAsync("DaysScreen"); 
            }
        }
        // Leo
        else
        {
            partsUI.SetActive(false);
            detailsUI.SetActive(false);
        }
    }

    public void OpenNotebook()
    {
        notebookUI.SetActive(true);
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
            MechaManager.instance.selectedChassi = false;
            MechaManager.instance.selectedLeftArm = false;
            MechaManager.instance.selectedRightArm = false;
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
        // Raycasting:
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Draw the ray from the camera's position
        Debug.DrawRay(ray.origin, ray.direction * 15, Color.blue);

        bool changing = MechaManager.instance.GetChangingPart;
        if (Physics.Raycast(ray, out hit, 15, layerMask) && !changing)
        {
            string partName = hit.transform.name;

            switch (partName)
            {
                case "RightArm":
                    MechaManager.instance.SelectBodyPart(MechaManager.Selected.RightArm);
                    break;
                case "Brand":
                    MechaManager.instance.SelectBodyPart(MechaManager.Selected.Brand);
                    break;
                case "LeftArm":
                    MechaManager.instance.SelectBodyPart(MechaManager.Selected.LeftArm);
                    break;
                default:
                    Debug.LogError("Selected BodyPart is not recognized");
                    break;
            }

            //Left to confirm
            if(Input.GetMouseButtonDown(0))
            {
                MechaManager.instance.ToggleChangingPart(true);
            }
        }

        //Right to return
        if(MechaManager.instance.GetChangingPart && Input.GetMouseButtonDown(1))
        {
            MechaManager.instance.ToggleChangingPart(false);
        }

        
    }
    
}
