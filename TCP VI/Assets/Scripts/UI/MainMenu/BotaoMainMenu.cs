using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoMainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator animator;

    void Start()
    {
        animator = GetComponentInParent<Animator>();    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Apontou");
        animator.SetTrigger("isMousePointing");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("isIdle");
    }
}
