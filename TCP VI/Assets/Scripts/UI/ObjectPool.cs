using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField] private List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] private int amountToPool = 4;

    [SerializeField] private GameObject[] silhouettePrefab;
    [SerializeField] private GameObject[] easterEggSilhouette;

    private void Awake()
    {
        // Verifica se não há nenhuma instância dessa classe. Se não tiver, usa a si mesma
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // Instancia os prefabs até ter a quantidade desejada e deixa eles desativados
        for(int i = 0; i < amountToPool; i++)
        {
            int randomIndex = Random.Range(0, silhouettePrefab.Length);
            int randomIndex2 = Random.Range(0, easterEggSilhouette.Length);

            GameObject obj = Instantiate(silhouettePrefab[randomIndex]);
            GameObject obj2 = Instantiate(easterEggSilhouette[randomIndex]);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
