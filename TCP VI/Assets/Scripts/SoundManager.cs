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
        if (instancia == null) instancia = this;
        else if (instancia != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    void Start()
    {
        SceneAudio(SceneManager.GetActiveScene());
    }
    public void litenfeito(AudioClip clipsom)
    {
        musicLoop.clip = clipsom;
        musicLoop.Play();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneAudio(scene);

    }
    void OnScene(Scene scene)
    {
        SceneAudio(scene);
    }
    void SceneAudio(Scene scene)
    {
        //Debug.Log("Nova cena carregada: " + scene.name); //Verifica nome cena

        if (scene.name == "CustomizationScene")
        {
            instancia.litenfeito(soundCustomizacao);
            return;
        }
        if (scene.name == "CombatScene")
        {
            instancia.litenfeito(soundCombat);
            return;
        }
        //Adicione novas musicas aqui com nome da cena
    }



}
