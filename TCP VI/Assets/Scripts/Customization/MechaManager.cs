using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MechaManager : MonoBehaviour
{
    public static MechaManager instance;

    public event Action< Selected, List<ArmSO>, List<BrandSO>, List<ArmSO> > ChangingMenuToggled;
    public event Action ChangingMenuUntoggled;
    public event Action<Selected, List<ArmSO>, List<BrandSO>, List<ArmSO> > PartSelected;
    public event Action<Selected> BodyPartChanged;
    public event Action<Selected> PartChanged;

    // Adicionado para passar ao billboard da Trainee e � c�mera qual parte foi selecionada
    public bool selectedLeftArm = false;
    public bool selectedRightArm = false;
    public bool selectedChassi = false;

    public enum Selected
    {
        RightArm, Brand, LeftArm
    }

    [Header("Selection Visualizer")]
    [SerializeField] Selected selectedBodyPart;
    [SerializeField] int selectedPartID;
    [SerializeField] bool changingPart = false;
    [SerializeField] bool choosePart = false;

    [SerializeField] ArmSO rightArmPart;
    [SerializeField] BrandSO brandPart;
    [SerializeField] ArmSO leftArmPart;
    //recommended: select defaults for these in the inspector, always (maybe?)

    [Header("Avaiable Parts")]
    [SerializeField] List<ArmSO> rightArms;
    [SerializeField] List<BrandSO> brands;
    [SerializeField] List<ArmSO> leftArms;
    
    private PlayerMecha playerMechaInstance;

    private void Awake()
    {
        //singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetupDefaultParts();
    }

    private void Update()
    {
        ChangePart();
    }

    private void SetupDefaultParts()
    {
        if (rightArms != null && leftArms != null && brands != null)
        {
            rightArmPart = rightArms[0];
            brandPart = brands[0];
            leftArmPart = leftArms[0];
        }
    }

    private int getPartsListSize(Selected _selectedBodyPart)
    {
        int partsListSize = 0; //default size
        switch (_selectedBodyPart)
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

    public void SelectNextBodyPart()
    {
        if (!this.changingPart)
        {
            Array enumValues = Enum.GetValues(typeof(Selected));
            int currentIndex = Array.IndexOf(enumValues, selectedBodyPart);

            int nextIndex = (currentIndex + 1) % enumValues.Length;
            selectedBodyPart = (Selected)enumValues.GetValue(nextIndex);
            BodyPartChanged?.Invoke(selectedBodyPart);
        }
    }

    public void SelectPreviousBodyPart()
    {
        if (!this.changingPart)
        {
            Array enumValues = Enum.GetValues(typeof(Selected));
            int currentIndex = Array.IndexOf(enumValues, selectedBodyPart);

            int prevIndex = (currentIndex - 1 + enumValues.Length) % enumValues.Length;
            // we are adding the length here because remainder operator would return -1 instead of 2
            selectedBodyPart = (Selected)enumValues.GetValue(prevIndex);
            BodyPartChanged?.Invoke(selectedBodyPart);
        }
    }

    public void SelectBodyPart(Selected _selectedBodyPart)
    {
        this.selectedBodyPart = _selectedBodyPart;
        BodyPartChanged?.Invoke(selectedBodyPart);
    }
    public void ToggleChangingPart(bool toggle)
    {
        bool wasToggled = changingPart;
        if (!choosePart)
        {
            changingPart = toggle;
        }

        if(!wasToggled && toggle)
        {
            ChangingMenuToggled?.Invoke(selectedBodyPart, leftArms, brands, rightArms);
            ResetSelectedPartID();
        }
        else if (!toggle)
        {
            ChangingMenuUntoggled?.Invoke();
        }
    }

    public void SelectNextPartID()
    {
        if (changingPart)
        {
            int partsListSize = getPartsListSize(selectedBodyPart);

            int currentIndex = selectedPartID;
            int nextIndex = (currentIndex + 1) % partsListSize;

            selectedPartID = nextIndex;
            PartSelected?.Invoke(selectedBodyPart, leftArms, brands, rightArms);
        }

    }

    public void SelectPreviousPartID()
    {
        if (changingPart)
        {
            int partsListSize = getPartsListSize(selectedBodyPart);

            int currentIndex = selectedPartID;
            int prevIndex = (currentIndex - 1 + partsListSize) % partsListSize;

            selectedPartID = prevIndex;
            PartSelected?.Invoke(selectedBodyPart, leftArms, brands, rightArms);
        }
    }

    public void SelectPartID(int partId)
    {
        if (changingPart)
        {
            selectedPartID = partId;
            PartSelected?.Invoke(selectedBodyPart, leftArms, brands, rightArms);

            selectedRightArm = false;
            selectedChassi = false;
            selectedLeftArm = false;
            // Muda as booleanas que ser�o utilizdas pelo Billboard da Trainee e da c�mera
            switch (selectedBodyPart)
            {
                case Selected.RightArm:
                    selectedRightArm = !selectedRightArm;
                    break;
                case Selected.Brand:
                    selectedChassi = !selectedChassi;
                    break;
                case Selected.LeftArm:
                    selectedLeftArm = !selectedLeftArm;
                    break;
                default:
                    Debug.LogError("Selected BodyPart is not recognized");
                    break;
            }
        }
    }

    public void ResetSelectedPartID()
    {
        SelectPartID(0);
    }

    public void ConfirmChoice()
    {
        if (changingPart)
        {
            choosePart = true;
        }
    }

    private void ChangePart()
    {
        if (choosePart)
        {
            switch (this.selectedBodyPart)
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
                    Debug.LogError("Selected BodyPart is not recognized");
                    break;
            }
            choosePart = false;

            PartChanged?.Invoke(selectedBodyPart);
            //basically copying Godot signals with eventActions here ;)
        }
    }

    #region ExternalR&W
    
    //can be called when enemy DIES
    public void AddPartToList(ScriptableObject newPart)
    {
        if (newPart is ArmSO newArmPart)
        {
            leftArms.Add(newArmPart);
            return;
        }

        if (newPart is BrandSO newBrandPart)
        {
            brands.Add(newBrandPart);
            return;
        }

        Debug.LogError("Error adding the desired part");
    }
    
    public void SetPlayerReference(PlayerMecha _player)
    {
        playerMechaInstance = _player;
    }

    public void SetMechaChanges()
    {
        //retrieve the SOs from this class and pass to others
        playerMechaInstance._rightArmSO = rightArmPart;
        playerMechaInstance._brandSO = brandPart;
        playerMechaInstance._leftArmSO = leftArmPart;
    }
    #endregion

    //below are PROPERTIES to acess the parts SOs (read-only) in other classes
    public bool GetChangingPart
    {
        get { return changingPart; }
    }
    public int GetSelectedPartID
    {
        get { return selectedPartID; }
    }
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
