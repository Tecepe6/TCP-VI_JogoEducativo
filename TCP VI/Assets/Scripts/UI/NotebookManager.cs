using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    public GameObject[] highlightedButtons;

    public void HighlightButton(int id)
    {
        for (int i = 0; i < highlightedButtons.Length; i++)
        {
            highlightedButtons[i].SetActive(i == id);
        }
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
