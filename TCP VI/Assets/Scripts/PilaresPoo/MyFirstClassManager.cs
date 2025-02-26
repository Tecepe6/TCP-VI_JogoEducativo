using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyFirstClassManager : MonoBehaviour
{
    public GameObject[] highlightedButtons;
    public POOPillar[] POOPillars;

    public POOPillar currentPillar;

    public TMP_Text timeCounter;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivateNotebook();
        }

        if(currentPillar != null)
        {
            timeCounter.gameObject.SetActive(true);
            timeCounter.text = ((int)currentPillar.CurrentTime).ToString();
            currentPillar.UpdatePillar();
        }
        else
        {
            timeCounter.gameObject.SetActive(false);
        }
    }

    public void HighlightButton(int id)
    {
        for (int i = 0; i < highlightedButtons.Length; i++)
        {
            highlightedButtons[i].SetActive(false);
        }

        if (!POOPillars[id].HasWon)
        {
            if (id == 0)
            {
                currentPillar = POOPillars[id];
                highlightedButtons[id].SetActive(true);
            }
            else if (POOPillars[id - 1].HasWon)
            {
                currentPillar = POOPillars[id];
                highlightedButtons[id].SetActive(true);
            }
            else
                currentPillar = null;
        }
    }


    public void CloseAll()
    {
        foreach (GameObject button in highlightedButtons)
        {
            currentPillar = null;
            button.SetActive(false);
        }
    }

    public void DeactivateNotebook()
    {
        gameObject.SetActive(false);
    }
}
