using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncaBotao : POOButton
{
    [SerializeField] PilarEncapsulamento _abstracao;

    [SerializeField] bool correta;
    [SerializeField] int maxLife;
    int life;

    public void Initialize()
    {
        SetDirection();
        gameObject.SetActive(true);

    }

    public void Click()
    {
        if (correta)
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
