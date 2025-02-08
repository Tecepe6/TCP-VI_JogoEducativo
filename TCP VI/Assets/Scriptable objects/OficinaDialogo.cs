using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OficinaDialogo : MonoBehaviour
{
    [SerializeField] GameObject[] canvasUI;
    private int num = 4;
    public void Dialogo()
    {
        num--;
        if (num <= -1)
        {
            SceneManager.LoadScene("CustomizationScene");

        }
        else
        {
            //Desculpa pra quem  estar vendo  isso....
            canvasUI[0].gameObject.SetActive(false);
            canvasUI[1].gameObject.SetActive(false);
            canvasUI[2].gameObject.SetActive(false);
            canvasUI[3].gameObject.SetActive(false);
            canvasUI[4].gameObject.SetActive(false);

            canvasUI[num].gameObject.SetActive(true);

        }

    }



}
