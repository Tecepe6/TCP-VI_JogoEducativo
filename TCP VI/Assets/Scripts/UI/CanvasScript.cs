using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    public void PauseInGame()
    {
        Time.timeScale = 0.0f;
        Pause();
    }
    public void Pause()
    {
        pauseUI.SetActive(true);
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void despausar()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Opcoes()
    {

    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1.0f && pauseUI != null)
        {
            PauseInGame();
        }
    }
    public void Revanche()
    {
        DaysManager.instance.LoadCurrentDay(); //edited, new system
    }
    public void customizacao()
    {
        SceneManager.LoadScene("CustomizationScene");
    }
    public void Sair()
    {
      Application.Quit();
    }
}
