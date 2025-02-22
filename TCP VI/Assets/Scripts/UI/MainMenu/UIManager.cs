using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isOnAnimation = false;

    [SerializeField] private GameObject[] canvaObject;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject telaSair;

    // Adiciona uma referência ao DialogueCanvas (se for um GameObject)
    [SerializeField] private GameObject dialogueCanvas;

    void Start()
    {
        telaSair.SetActive(false);

        if (dialogueManager == null)
        {
            dialogueManager = FindObjectOfType<DialogueManager>();
            if (dialogueManager == null)
            {
                Debug.LogError("DialogueManager não foi encontrado!");
            }
        }

        if (animator == null)
        {
            Debug.LogError("Animator não atribuído!");
        }

        if (dialogueCanvas == null)
        {
            Debug.LogError("DialogueCanvas não foi atribuído!");
        }
    }

    void Update()
    {
        if (isOnAnimation && Input.GetKeyDown(KeyCode.Space))
        {
            OutroAnim();
        }

        if(isOnAnimation == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                OutroAnim();
            }
        }
    }

    // FUNÇÕES DOS BOTÕES
    // Só no MainMenu
    public void IntroAnim()
    {
        isOnAnimation = true;
        dialogueManager.dialogueParent.SetActive(true);

        for (int i = 0; i < canvaObject.Length; i++)
        {
            canvaObject[i].SetActive(false);
        }

        // Verifique se o DialogueCanvas foi atribuído corretamente
        if (dialogueCanvas != null)
        {
            Animator dialogueCanvasAnimator = dialogueCanvas.GetComponent<Animator>();
            if (dialogueCanvasAnimator != null)
            {
                dialogueCanvasAnimator.SetTrigger("intro");
            }
            else
            {
                Debug.LogError("Animator não encontrado no DialogueCanvas!");
            }
        }
        else
        {
            Debug.LogError("DialogueCanvas não foi atribuído no Inspector!");
        }
    }

    public void OutroAnim()
    {
        Animator dialogueCanvasAnimator = dialogueCanvas.GetComponent<Animator>();
        dialogueCanvasAnimator.SetTrigger("outro");
    }

    // Funções gerais para navegação entre cenas
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoToLoadingScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    // Funções de sair
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