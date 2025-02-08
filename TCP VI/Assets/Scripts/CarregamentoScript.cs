using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class CarregamentoScript : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Carregamento());
    }
    IEnumerator Carregamento()
    {
        //pode tirar dps se quiser
        yield return new WaitForSecondsRealtime(3f);

        AsyncOperation operation = SceneManager.LoadSceneAsync("Apresentacao");
    }
}
