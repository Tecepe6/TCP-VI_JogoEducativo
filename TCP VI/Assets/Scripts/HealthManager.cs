using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Combatant combatant;

    public Image healthBarImage;

    private void Start()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        float healthPercent = (float)combatant.currentLife / combatant._brandSO.MaxLife;

        healthPercent = Mathf.Clamp01(healthPercent);

        healthBarImage.fillAmount = healthPercent;
    }
}
