using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CombateManager : MonoBehaviour
{
    private bool inicioCombat;
    [SerializeField] GameObject controleUI;
    [SerializeField] GameObject[] uis;
    [SerializeField] TextMeshProUGUI textoTempo;
    [SerializeField] float tempo;

    [SerializeField] Combatant combatantScript;

    [SerializeField] GameObject telaDePause;
    private bool isPaused = false;

    [SerializeField] bool instrucaoConcluida = false;

    void Start()
    {
        instrucaoConcluida = false;

        Time.timeScale = 0.0f;
        inicioCombat = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        combatantScript.enabled = false;

        telaDePause.SetActive(false);
    }

    void Update()
    {
        if (inicioCombat && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !isPaused)
        {
            inicioCombat = false;
            controleUI.SetActive(false);
            foreach (GameObject u in uis)
            {
                if (u != null)
                    u.SetActive(true);
            }

            StartCoroutine(Contagem());
        }

        // Chama a função de menu de pausa
        CheckPauseInput();
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

        instrucaoConcluida = true;

        combatantScript.enabled = true;
    }

    // MENU DE PAUSA
    public void CheckPauseInput()
    {
        // Verifica se o jogador pressionou o ESC para alternar pausa/despausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (!isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        if (instrucaoConcluida)
        {
            // Pausa o jogo
            isPaused = true;
            Time.timeScale = 0f;  // Congela o tempo

            telaDePause.SetActive(true);  // Exibe o menu de pausa
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Desliga script do jogador
            combatantScript.enabled = false;
        }
    }

    public void ResumeGame()
    {
        // Retoma o jogo
        isPaused = false;
        Time.timeScale = 1f;  // Volta o tempo ao normal

        // Reativa o script do jogador
        combatantScript.enabled = true;

        telaDePause.SetActive(false);

        ClosePauseMenu();
    }

    public void ClosePauseMenu()
    {
        telaDePause.SetActive(false);  // Esconde o menu de pausa
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Troca de cenas
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToCustomizationScene()
    {
        SceneManager.LoadScene("CustomizationScene");
    }

    // Fechar o jogo
    public void CloseGame()
    {
        Application.Quit();
    }
}
