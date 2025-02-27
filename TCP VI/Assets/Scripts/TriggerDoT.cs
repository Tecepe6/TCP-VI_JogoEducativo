using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoT : MonoBehaviour
{
    [SerializeField] DialogueTrigger[] dialogueTriggers;
    [SerializeField] int contadorDias = 0;
    void Start()
    {
        if(contadorDias == 0)
        {
            contadorDias++;
            dialogueTriggers[0].TriggerDialogue();
        }
        else if(contadorDias == 1)
        {
            contadorDias++;
            dialogueTriggers[1].TriggerDialogue();
        }
        else
        {
            dialogueTriggers[2].TriggerDialogue();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
