using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaDialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private Animator animator;

    [SerializeField] private UIManager uiManager;
    public void TriggerIntroDialogue()
    {
        dialogueTrigger.TriggerDialogue();
        animator.SetTrigger("introPlaying");
    }

    public void UseGoToCustomization()
    {
        uiManager.GoToCustomization();
    }
}
