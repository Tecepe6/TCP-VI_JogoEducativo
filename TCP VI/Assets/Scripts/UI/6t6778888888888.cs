using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class CombateManager : MonoBehaviour
{
    private bool inicioCombat;
    [SerializeField] GameObject controleUI;
    [SerializeField] GameObject[] uis;
    [SerializeField] TextMeshProUGUI textoTempo;
    [SerializeField] float tempo;
    void Start()
    {
        Time.timeScale = 0.0f;
        inicioCombat = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        if (inicioCombat && Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) && inicioCombat)
        {
            inicioCombat = false;
            controleUI.SetActive(false);
            foreach (GameObject u in uis)
            {
                if(u!= null)
                u.SetActive(true);
            }

            StartCoroutine(Contagem());
        }
    }


    IEnumerator Contagem()
    {

        textoTempo.gameObject.SetActive(true);
        textoTempo.text = "3";
        yield return new WaitForSecondsRealtime(tempo);
        textoTempo.text = "2";
        yield return new WaitForSecondsRealtime(tempo);
        textoTempo.text = "1";
        yield return new WaitForSecondsRealtime(tempo);
        textoTempo.text = "Lutem!";
        yield return new WaitForSecondsRealtime(tempo);
        textoTempo.gameObject.SetActive(false);
        Time.timeScale = 1.0f;

    }
}
