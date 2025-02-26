using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarAbstracao : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] float timeFrame;
    float currentTime;

    public bool StillActive => currentTime > 0;

    [SerializeField] int pontuacao;

    private void Start()
    {
        currentTime = timeFrame;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        
        if(StillActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GenerateRaycast();
            }
        }
    }

    private void GenerateRaycast()
    {
        Transform cameraTransform = Camera.main.transform;
        if (Physics.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), cameraTransform.forward, out RaycastHit hit, 15f, layerMask))
        {
            if (hit.collider.TryGetComponent(out AbstBotao abstracao))
            {
                pontuacao++;
                Debug.Log(abstracao.ReturnAbstracrionValue());
            }
        }
    }
}
