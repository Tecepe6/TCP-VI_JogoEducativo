using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApresentacaoScript : MonoBehaviour
{
    [SerializeField] float tempo;
    float contador;
    void Update()
    {
        contador += Time.deltaTime;
        if (contador >= tempo)
        {
            SceneManager.LoadScene("CombatScene");
        }
    }
}
