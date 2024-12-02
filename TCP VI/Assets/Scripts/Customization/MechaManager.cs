using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;



public class MechaManager : MonoBehaviour
{
    public static MechaManager instance;

    public event Action PartChanged;

    private enum Selected
    {
        RightArm, Brand, LeftArm
    }

    [Header ("Selection Visualizer")]
    [SerializeField] Selected selectedBodyPart;
    [SerializeField] int selectedPartID;
    [SerializeField] bool changingPart = false;
    [SerializeField] bool choosePart = false;
    
    [SerializeField] ArmSO rightArmPart;
    [SerializeField] BrandSO brandPart;
    [SerializeField] ArmSO leftArmPart;

    
    [Header ("UI Components")]
    [SerializeField] GameObject partsUI;
    [SerializeField] GameObject detailsUI;
    
    [Header ("Avaiable Parts")]
    [SerializeField] List<ArmSO> rightArms;
    [SerializeField] List<BrandSO> brands;
    [SerializeField] List<ArmSO> leftArms;

    private void Awake() 
    {
        //singleton managing
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        //getting UI components
        partsUI = getUIComponents("/Canvas/PartsUI");
        detailsUI = getUIComponents("/Canvas/DetailsUI");
    }

    private void Start() 
    {
        SetupDefaultParts();
    }

    private void Update() 
    {
        // TODO: Clean UI Controller Stuff
        
        //Selecting
        if (Input.GetKeyDown(KeyCode.A))
        {
            SelectPreviousEnum();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SelectNextEnum();

            Debug.Log($"{selectedBodyPart}");
        }

        //Go back and forth the ChangingParts Menu
        UIVisibility();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            changingPart = true;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            changingPart = false;
            ResetSelectedPartID();
        }

        //Go up and down our list of parts
        if (Input.GetKeyDown(KeyCode.W))
        {
            SelectPreviousPartID();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SelectNextPartID();
        }

        //Confirm an option
        if (Input.GetKeyUp(KeyCode.Z))
        {
            ConfirmChoice();
        }

        ChangePart();

    }

    private void UIVisibility()
    {
        if(changingPart)
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

    private GameObject getUIComponents(string compenentName)
    {
        GameObject uiComponent = GameObject.Find(compenentName);
        if(uiComponent == null)
        {
            Debug.LogError($"<color=red>[DEV_ERROR] Cannot Find {compenentName} object in scene.");
            return null;
        }
        else
        {
            return uiComponent;
        }
    }

    private void SetupDefaultParts()
    {
        if(rightArms != null && leftArms != null && brands !=null)
        {
            rightArmPart = rightArms[0];
            brandPart = brands[0];
            leftArmPart = leftArms[0];
        }
    }

    private int getPartsListSize(Selected _selectedBodyPart)
    {
        int partsListSize = 0; //default size
        switch(_selectedBodyPart) 
        {
            
            case Selected.RightArm:
                partsListSize = rightArms.Count;
                break;
            case Selected.Brand:
                partsListSize = brands.Count;
                break;
            case Selected.LeftArm:
                partsListSize = leftArms.Count;
                break;
            default:
                Debug.LogError("Selected Part is out of scope");
                break;
        }

        return partsListSize;
    }

    private void SelectNextEnum()
    {
        if(!this.changingPart)
        {
            Array enumValues = Enum.GetValues(typeof(Selected));
            int currentIndex = Array.IndexOf(enumValues, selectedBodyPart);

            int nextIndex = (currentIndex + 1) % enumValues.Length;
            selectedBodyPart = (Selected)enumValues.GetValue(nextIndex);
        }
    }

    private void SelectPreviousEnum()
    {
        if(!this.changingPart)
        {
            Array enumValues = Enum.GetValues(typeof(Selected));
            int currentIndex = Array.IndexOf(enumValues, selectedBodyPart);

            int prevIndex = (currentIndex - 1 + enumValues.Length) % enumValues.Length; 
            // we are adding the length here because remainder operator would return -1 instead of 2
            selectedBodyPart = (Selected)enumValues.GetValue(prevIndex);
        }
    }

    private void SelectNextPartID()
    {
        if(changingPart)
        {
            int partsListSize = getPartsListSize(selectedBodyPart);

            int currentIndex = selectedPartID;
            int nextIndex = (currentIndex + 1) % partsListSize;
                
            selectedPartID = nextIndex;
        }
        
    }

    private void SelectPreviousPartID()
    {
        if(changingPart)
        {
            int partsListSize = getPartsListSize(selectedBodyPart);

            int currentIndex = selectedPartID;
            int prevIndex = (currentIndex - 1 + partsListSize) % partsListSize;
                
            selectedPartID = prevIndex;
        }
    }

    private void SelectPartID(int partId)
    {
        if(changingPart)
        {
            selectedPartID = partId;
        }
    }

    private void ResetSelectedPartID()
    {
        selectedPartID = 0;
    }


    public void ConfirmChoice()
    {
        if(changingPart)
        {
            choosePart = true;
        }
    }

    private void ChangePart()
    {
        if(choosePart)
        {
            switch(this.selectedBodyPart) 
            {
                case Selected.RightArm:
                    this.rightArmPart = rightArms[selectedPartID];
                    break;
                case Selected.Brand:
                    this.brandPart = brands[selectedPartID];
                    break;
                case Selected.LeftArm:
                    this.leftArmPart = leftArms[selectedPartID];
                    break;
                default:
                    Debug.LogError("Selected Part is out of scope");
                    break;
            }
            choosePart = false;
            PartChanged?.Invoke(); // basically copying Godot signals here ;)
        }
    }
    
    //below are PROPERTIES to acess the parts SOs (read-only) in other classes
    public ArmSO GetRightArm
    {
        get { return rightArmPart; }
    }
    public BrandSO GetBrand
    {
        get { return brandPart; }
    }
    public ArmSO GetLeftArm
    {
        get { return leftArmPart; }
    }
    


}
