using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaDialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private Animator animator;

    [SerializeField] private UIManager uiManager;

    void Start()
    {
        // Resetando o estado do animator
        ResetAnimatorTriggers();
        Debug.Log("RESETOU");
    }
    public void TriggerIntroDialogue()
    {
        dialogueTrigger.TriggerDialogue();
        animator.SetTrigger("introPlaying");
    }

    public void UseGoToLoadingScene()
    {
        uiManager.GoToLoadingScene();
    }

    public void ResetAnimatorTriggers()
    {
        animator.ResetTrigger("intro");
        animator.ResetTrigger("outro");
        animator.ResetTrigger("introPlaying");
    }
}
