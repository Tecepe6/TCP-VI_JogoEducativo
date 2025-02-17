using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject telaDePause;
    private bool isPaused = false;

    [SerializeField] GameObject telaCustomizacao;
    [SerializeField] MechaManager mechaManager;

    void Start()
    {
        telaDePause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (!isPaused && !mechaManager.GetChangingPart)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        telaDePause.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        telaCustomizacao.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        telaDePause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        telaCustomizacao.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToCustomizationScene()
    {
        SceneManager.LoadScene("CustomizationScene");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void GoToDayScreen()
    {
        SceneManager.LoadScene("GoToDayScreen");
    }
}