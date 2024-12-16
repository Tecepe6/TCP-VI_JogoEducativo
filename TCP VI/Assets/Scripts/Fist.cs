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
            opponent.TakeDamage(currentDamage, this.tipoDano);
            
            Debug.Log("colis�o" + gameObject.name);
        }
    }

    public void QuickDamage()
    {
        currentDamage = combatant._rightArmSO.QuickDamage;
        tipoDano = 1;

        Debug.Log("bra�oFraco" + gameObject.name);
    }

    public void StrongDamage()
    {
        currentDamage = combatant._leftArmSO.StrongDamage;
        tipoDano = 2;

        Debug.Log("bra�oForte" + gameObject.name);
    }
}
