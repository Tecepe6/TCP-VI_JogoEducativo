using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    public GameObject[] highlightedButtons;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivateNotebook();
        }
    }

    public void HighlightButton(int id)
    {
        for (int i = 0; i < highlightedButtons.Length; i++)
        {
            highlightedButtons[i].SetActive(i == id);
        }
    }

    public void DeactivateNotebook()
    {
        gameObject.SetActive(false);
    }
}
