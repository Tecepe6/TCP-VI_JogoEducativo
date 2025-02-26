using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstButtonManager : MonoBehaviour
{
    [SerializeField] AbstBotao[] metodos;

    public void StartButtons()
    {
        foreach (AbstBotao metodo in metodos)
        {
            metodo.Initialize();
        }
    }
}
