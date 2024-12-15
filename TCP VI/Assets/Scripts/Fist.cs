using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    Combatant combatant;

    public string tipoDeDano;

    public int currentDamage;

    void Start()
    {
        combatant = GetComponentInParent<Combatant>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.TryGetComponent(out Combatant opponent))
        {
            opponent.TakeDamage(currentDamage, tipoDeDano);
        }
    }

    public void QuickDamage()
    {
        currentDamage = combatant._rightArmSO.QuickDamage;
        tipoDeDano = "Dano Fraco";
    }

    public void StrongDamage()
    {
        currentDamage = combatant._leftArmSO.StrongDamage;
        tipoDeDano = "Dano Forte";
    }
}
