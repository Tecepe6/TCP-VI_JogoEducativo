using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DaysManager : MonoBehaviour
{
    public static DaysManager instance;
    public int currentDay = 1;
    [Header("Dicionário de Dias e Cenas")]
    [SerializedDictionary("Day","Scene")] public SerializedDictionary<int, string> gameDays;

    public void UnlockDay(int nextDay)
    {
        if(nextDay > this.currentDay)
        {
            this.currentDay = nextDay;
        }
    }

    public void LoadCurrentDay()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(this.gameDays[currentDay]); 
    }

    public void LoadDay(int dayID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(this.gameDays[dayID]); 
    }

    public int getCurrentDay()
    {
        return currentDay;
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
        if (gameDays.Count == 0)
        {
            gameDays.Add(1, "CustomizationScene");
        }
    }
}
