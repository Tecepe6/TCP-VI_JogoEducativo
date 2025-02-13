using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoMainMenu : MonoBehaviour
{
    public float moveDistance = 20f; // Distância para mover o botão
    public float fadeSpeed = 5f; // Velocidade da transição de opacidade
    public float moveSpeed = 5f; // Velocidade de movimentação
    public float targetOpacity = 1f; // Opacidade alvo ao passar o mouse
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Vector2 targetPosition;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        targetPosition = originalPosition + new Vector2(moveDistance, 0); // Movendo para a direita
    }

    public void OnMouseEnter()
    {
        StopAllCoroutines();
        StartCoroutine(FadeAndMove(targetOpacity, targetPosition));
    }

    public void OnMouseExit()
    {
        StopAllCoroutines();
        StartCoroutine(FadeAndMove(0.5f, originalPosition)); // Volta à opacidade original (50%)
    }

    private System.Collections.IEnumerator FadeAndMove(float targetAlpha, Vector2 targetPos)
    {
        while (Mathf.Abs(canvasGroup.alpha - targetAlpha) > 0.01f || Vector2.Distance(rectTransform.anchoredPosition, targetPos) > 0.1f)
        {
            // Alterar opacidade
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);

            // Mover botão
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPos, moveSpeed * Time.deltaTime);

            yield return null;
        }

        // Garantir que os valores finais sejam atingidos
        canvasGroup.alpha = targetAlpha;
        rectTransform.anchoredPosition = targetPos;
    }
}
