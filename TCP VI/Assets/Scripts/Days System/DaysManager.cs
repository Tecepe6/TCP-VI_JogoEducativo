using System.Collections;
using System.Collections.Generic;

using AYellowpaper.SerializedCollections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DaysManager : MonoBehaviour
{
    public static DaysManager instance;
    [SerializeField] int currentDay = 1;
    [Header("Dicionário de Dias e Cenas")]
    [SerializedDictionary("Day","Scene")] public SerializedDictionary<int, SceneAsset> gameDays;

    public void UnlockDay(int nextDay)
    {
        if(nextDay > this.currentDay)
        {
            this.currentDay = nextDay;
        }
    }

    public void LoadCurrentDay()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(this.gameDays[currentDay].name); 
    }

    private void Singleton()
    {
        
        //singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Awake()
    {
        Singleton();

        //in case we dont add the first day
        SceneAsset firstScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/Apresentacao.unity");

        if (gameDays.Count == 0)
        {
            gameDays.Add(1, firstScene);
        }

        Debug.Log("testeee: " + this.gameDays[currentDay]);
    }
}
