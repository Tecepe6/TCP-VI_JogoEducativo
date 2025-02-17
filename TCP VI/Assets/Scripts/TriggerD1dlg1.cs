using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerD1dlg1 : MonoBehaviour
{
    [SerializeField] DialogueTrigger dialogueTrigger;
    public void Start()
    {
        if(dialogueTrigger != null)
        {
            dialogueTrigger.TriggerDialogue();
        }
        else
        {
            Debug.LogWarning("Não identificou Dialogue Trigger");
        }
    }

    void Update()
    {
        
    }
}
