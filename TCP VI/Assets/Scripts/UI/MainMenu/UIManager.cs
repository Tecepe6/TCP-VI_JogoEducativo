using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    private bool isOnAnimation = false;

    [SerializeField] GameObject[] canvaObject;

    [SerializeField] DialogueManager dialogueManager;

    [SerializeField] GameObject telaSair;

    void Start()
    {
        telaSair.SetActive(false);

        if (dialogueManager == null)
        {
            dialogueManager = FindObjectOfType<DialogueManager>();
            if (dialogueManager == null)
            {
                Debug.LogError("DialogueManager n�o foi encontrado!");
            }
        }
    }

    public void Update()
    {
        if (isOnAnimation)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OutroAnim();                
            }
        }
    }

    // FUN��ES DOS BOT�ES
    // S� no MainMenu
    public void IntroAnim()
    {
        isOnAnimation = true;
        dialogueManager.dialogueParent.SetActive(true);

        for(int i = 0; i < canvaObject.Length; i++)
        {
            canvaObject[i].gameObject.SetActive(false);
        }
        animator.SetTrigger("intro");
    }

    public void OutroAnim()
    {
        animator.SetTrigger("outro");
    }

    // Gerais
    public void GoToCustomizationScene(string sceneName)
    {
        SceneManager.LoadScene("CustomizationScene");
    }

    public void GoToCombatScene(string sceneName)
    {
        SceneManager.LoadScene("CombatScene");
    }

    public void GoToMainMenu(string sceneName)
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Sair()
    {
        telaSair.SetActive(true);
    }

    public void SairNao()
    {
        telaSair.SetActive(false);
    }

    public void SairSim()
    {
        Application.Quit();
        Debug.Log("Fechou o jogo!");
    }
}
