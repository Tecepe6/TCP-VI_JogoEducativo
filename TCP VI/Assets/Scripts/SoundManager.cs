using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicLoop;
    public static SoundManager instancia = null;
    public AudioClip soundCustomizacao;
    public AudioClip soundCombat;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instancia != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        // Certifique-se de que a música esteja tocando ao iniciar no MainMenu
        SceneAudio(SceneManager.GetActiveScene());
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneAudio(scene);
    }

    // Função para reproduzir música, verificando se o clipe já está tocando
    public void PlaySound(AudioClip clip)
    {
        if (musicLoop != null && clip != null)
        {
            // Verifica se o clipe atual é diferente
            if (musicLoop.clip != clip)
            {
                musicLoop.clip = clip;
                musicLoop.Play();
            }
        }
    }

    void SceneAudio(Scene scene)
    {
        if (scene.name == "MainMenu" || scene.name == "CustomizationScene")
        {
            instancia.PlaySound(soundCustomizacao);
            return;
        }
        if (scene.name == "CombatScene" || scene.name =="2_CombatScene" || scene.name == "3_CombatScene")
        {
            instancia.PlaySound(soundCombat);
            return;
        }
    }
}
