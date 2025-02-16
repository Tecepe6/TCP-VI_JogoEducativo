using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PartButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] int partID;
    [SerializeField] string partName;
    [SerializeField] TextMeshProUGUI partsText;
    private Button buttonComponent;

    void Awake()
    {
        partsText = GetComponentInChildren<TextMeshProUGUI>();

        UpdateText(partName);
    }

    private void Start() 
    {
        buttonComponent = this.GetComponent<Button>();
        buttonComponent.onClick.AddListener(ClickAction);
    }
    
    public void SetButtonData(int _partId, string _partName)
    {
        partID = _partId;
        partName = _partName;
        //uncomment if names arent showing correctly:
        //UpdateText(partName);
    }
    
    private void UpdateText(string newText)
    {
        this.partsText.text = newText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MechaManager.instance.SelectPartID(this.partID);
    }

    public void OnSelect (BaseEventData eventData) 
	{	
        MechaManager.instance.SelectPartID(this.partID);
	}

    void ClickAction()
    {
        MechaManager.instance.ConfirmChoice();
    }
}
