using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhuetasMenu : MonoBehaviour
{
    [SerializeField] float speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TraverseScreen()
    {
        GameObject silhouette = ObjectPool.instance.GetPooledObject();
    }
}
