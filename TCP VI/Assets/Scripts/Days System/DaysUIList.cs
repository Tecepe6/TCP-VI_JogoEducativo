using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEditor;
using UnityEngine;

public class DaysUIList : MonoBehaviour
{
    [Header ("Prefab and ButtonsParent")]
    [SerializeField] GameObject buttonsPrefab;
    [SerializeField] Transform content;

    [SerializeField] List<GameObject> buttons;
    

    void Start()
    {
        CreateButtons();

    }

    private void CreateButtons()
    {
        int currentDay = DaysManager.instance.getCurrentDay();

        foreach(KeyValuePair<int, UnityEditor.SceneAsset> gameDay in DaysManager.instance.gameDays)
        {
            if(gameDay.Key <= currentDay)
            {
                GameObject newButton = Instantiate(buttonsPrefab, content);

                DayButton dayButton = newButton.GetComponent<DayButton>();                
                dayButton.SetButtonData(gameDay.Key);

                buttons.Add(newButton);  
            }
        }
    }

}
