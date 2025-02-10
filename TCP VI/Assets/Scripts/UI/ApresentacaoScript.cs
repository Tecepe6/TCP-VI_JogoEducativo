using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApresentacaoScript : MonoBehaviour
{
    [SerializeField] float tempo;

    void Start()
    {
        StartCoroutine(Carregamento());
    }

    IEnumerator Carregamento()
    {
        //pode tirar dps se quiser
        yield return new WaitForSecondsRealtime(tempo);

        AsyncOperation operation = SceneManager.LoadSceneAsync("CombatScene");
    }
}
