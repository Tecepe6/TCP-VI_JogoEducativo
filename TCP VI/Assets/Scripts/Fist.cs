using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    Combatant combatant;

    public int tipoDano;

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
            opponent.TakeDamage(currentDamage, tipoDano);
        }
    }

    public void QuickDamage()
    {
        currentDamage = combatant._rightArmSO.QuickDamage;
        tipoDano = 1;
    }

    public void StrongDamage()
    {
        currentDamage = combatant._leftArmSO.StrongDamage;
        tipoDano = 2;
    }
}
