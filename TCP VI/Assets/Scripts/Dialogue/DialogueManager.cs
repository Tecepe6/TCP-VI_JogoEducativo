using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

[ExecuteAlways] //Executar o código no editor;
public class DialogueManager : MonoBehaviour
{
    // Leo
    public bool isInDialogue = false;

    [Header("Objetos")]
    // LEO ALTEROU O TIPO DE PROTEÇÃO PARA ATIVAR PELO UIMANAGER
    /*[SerializeField]*/ public GameObject dialogueParent;
    [SerializeField] GameObject dialogueBG;
    [SerializeField] GameObject promptWindow;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text characterText;
    [SerializeField] Image character1Image;
    [SerializeField] Image character2Image;
    [SerializeField] Button option1Button;
    [SerializeField] Button option2Button;
    [SerializeField] Button nextButton;
    [SerializeField] GameObject skipButton;

    [Header("Primeiro a Selecionar(UI)")]
    [SerializeField] private GameObject _dialogueMenuFirst;
    
    [Header("Velocidade do texto")]
    [SerializeField] float typingSpeed = 0.05f;

    [Header("Config Atual:")]
    private DialogueHolder.Dialogue dialogue;
    [SerializeField] CharacterSetSO characterSet;
    [SerializeField] int currentDialogueIndex = 0;

    void Start()
    {
        dialogueParent.SetActive(false);
        dialogueBG.SetActive(false);
        character1Image.enabled = false;
        character2Image.enabled = false;
        promptWindow.SetActive(false);
        skipButton.SetActive(false);
    }

    public void DialogueStart(DialogueHolder.Dialogue textToPrint, int dialogueStartIndex, int dialogueEndIndex)
    {
        dialogueParent.SetActive(true);
        dialogueBG.SetActive(true);
        character1Image.enabled = true;
        character2Image.enabled = true;
        promptWindow.SetActive(true);
        skipButton.SetActive(true);

        // TODO: STOP inputs and/ or movement DURING dialogue
        // Leo
        isInDialogue = true;
        // here
        
        dialogue = textToPrint; //passing data in Holder Class
        currentDialogueIndex = dialogueStartIndex;
        ShowButtons(false);

        StartCoroutine(PrintDialogue(dialogueEndIndex));
    }

    private void ShowButtons(bool showButtons)
    {
        //set buttons visibility and interactivity
        
        option1Button.interactable = showButtons;
        option2Button.interactable = showButtons;
        if(!showButtons)
        {
            option1Button.GetComponentInChildren<TMP_Text>().text = "";
            option2Button.GetComponentInChildren<TMP_Text>().text = "";
            nextButton.GetComponentInChildren<TMP_Text>().text = "Próximo";
        }
        else
        {
            nextButton.GetComponentInChildren<TMP_Text>().text = "";
        }

        EventSystem.current.SetSelectedGameObject(_dialogueMenuFirst); // manda a  UI selecionar o primeiro botão
    }

    private void SetButtonsText(DialogueHolder.DialogueLine dialogueLine)
    {
        if (dialogueLine.options != null && dialogueLine.options.Length == 2)
        {
            option1Button.GetComponentInChildren<TMP_Text>().text = dialogueLine.options[0];
            option2Button.GetComponentInChildren<TMP_Text>().text = dialogueLine.options[1];
        }
        else
        {
            // If options are not defined or there are not enough options, set empty text
            option1Button.GetComponentInChildren<TMP_Text>().text = "";
            option2Button.GetComponentInChildren<TMP_Text>().text = "";
        }
    }

    private bool optionSelected = false;

    private IEnumerator PrintDialogue(int dialogueEndIndex)
    {
        Debug.Log("Diálogo" +
        $"Inicio: {currentDialogueIndex}, Fim:{dialogueEndIndex}");

        while(currentDialogueIndex < dialogueEndIndex + 1)
        {
            DialogueHolder.DialogueLine line = dialogue.dialogueLines[currentDialogueIndex];
            if(!String.IsNullOrEmpty(line.character) && !String.IsNullOrEmpty(line.expression))
            {
                ChangeCharacterImage(character1Image, line.character, line.expression);
                ChangeCharacterName(line.character);
            }
            
            if(String.IsNullOrEmpty(line.character2))
            {
                character2Image.enabled = false;
                promptWindow.SetActive(false);
            }

            if(!String.IsNullOrEmpty(line.character2) && !String.IsNullOrEmpty(line.expression2))
            {
                character2Image.enabled = true;
                promptWindow.SetActive(true);
                ChangeCharacterImage(character2Image, line.character2, line.expression2);
                
            }
            
            

            if(line.options != null && line.goTo != null && line.goTo.Length >= 2) //check if it has options of answer
            {
                yield return StartCoroutine(TypeText(line.text));
                
                ShowButtons(true);
                SetButtonsText(line);

                int goTo1 = GetDialogueIndex(line.goTo[0]);
                int goTo2 = GetDialogueIndex(line.goTo[1]);
                option1Button.onClick.AddListener(() => HandleOptionSelected(goTo1));
                option2Button.onClick.AddListener(() => HandleOptionSelected(goTo2));

                yield return new WaitUntil(() => optionSelected);
            }
            else
            {
                
                yield return StartCoroutine(TypeText(line.text));
                
                //Pressionar para passar:
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonUp(0));
            }

            optionSelected = false;

            if (currentDialogueIndex > dialogueEndIndex)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonUp(0));
            }
        }

        DialogueStop();

    }

    private void HandleOptionSelected(int goToIndex)
    {
        optionSelected = true;
        ShowButtons(false);

        currentDialogueIndex = goToIndex;
    }
    
    private IEnumerator TypeText(string text)
    {
        DialogueHolder.DialogueLine line = dialogue.dialogueLines[currentDialogueIndex];
        dialogueText.text = "";
        foreach(char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
            
        }

        if(line.options == null) //check if it hasn't any options
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonUp(0));
        }
        
        if(dialogue.dialogueLines[currentDialogueIndex].isEnd)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonUp(0));
            DialogueStop();
        }

        currentDialogueIndex++;
        Debug.Log($"Incrementou current: {currentDialogueIndex}");
    }

    private void ChangeCharacterName(string characterName)
    {
        string SOPAth = "CharacterSets/" + characterName;
        this.characterSet = Resources.Load<CharacterSetSO>(SOPAth);

        characterText.text = characterSet.CharacterName;
    }

    private void ChangeCharacterImage(Image characterImage, string characterName, string expression)
    {
        string SOPAth = "CharacterSets/" + characterName;
        this.characterSet = Resources.Load<CharacterSetSO>(SOPAth);

        if(characterImage == character2Image && characterName != "DEMO")
        {
            characterImage.transform.Rotate(0, 180, 0);
        }
        characterImage.sprite = characterSet.Expressions[expression];
    }

    private void DialogueStop()
    {
        StopAllCoroutines(); //stop all text typing
        dialogueText.text = "";
        dialogueParent.SetActive(false);
        dialogueBG.SetActive(false);
        character1Image.enabled = false;
        character2Image.enabled = false;
        promptWindow.SetActive(false);
        skipButton.SetActive(false);
        
        EventSystem.current.SetSelectedGameObject(null); //deseleciona
        
        // TODO: RESTART inputs when Dialogue STOPS;
        // Leo
        isInDialogue = false;
        // here
    }

    public int GetDialogueIndex(string id)
    {
        for (int i = 0; i < dialogue.dialogueLines.Length; i++)
        {
            if (dialogue.dialogueLines[i].id == id)
            {
                return i; //index of the CURRENT dialogueLine
            }
        }
        return -1; //Out of Bounds
    }

    public void SkipDialogue()
    {
        // Define o index do diálogo como o último
        currentDialogueIndex = dialogue.dialogueLines.Length - 1;
    }
}
