using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayButton : MonoBehaviour
{
    [SerializeField] int dayID;
    [SerializeField] TextMeshProUGUI dayText;
    private Button buttonComponent;
    
    void Awake()
    {
        dayText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateText();
    }

    private void Start() 
    {
        buttonComponent = this.GetComponent<Button>();
        buttonComponent.onClick.AddListener(ClickAction);
    }

    public void SetButtonData(int _dayId)
    {
        dayID = _dayId;
        UpdateText();
    }

    private void UpdateText()
    {
        if(dayID >= 1)
        {
            this.dayText.text = "Dia: " + dayID;
        }
        else if (dayID == 0)
        {
            this.dayText.text = "Voltar...";
        }
    }

    private void ClickAction()
    {
        //load corresponding scene of the day:
        if(dayID >= 0)
        {
            DaysManager.instance.LoadDay(this.dayID);
        }
        
    }

}
