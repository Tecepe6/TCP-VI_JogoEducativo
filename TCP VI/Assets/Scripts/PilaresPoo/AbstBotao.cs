using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstBotao : POOButton
{
    [SerializeField] PilarAbstracao _abstracao;

    [SerializeField] bool correta;


    public void Initialize()
    {
        SetDirection();
        gameObject.SetActive(true);
        
    }

    public void Click()
    {
        if(correta)
            _abstracao.AddPoints(1);
        else
            _abstracao.AddPoints(-1);

        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Move();
    }
}
