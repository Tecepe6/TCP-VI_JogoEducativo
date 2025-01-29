using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour
{
    Combatant combatant;

    // Serve para identificara diferenciar os danos para que o mecha oponente toque a anima��o correspondente
    public int tipoDano;

    public int currentDamage;

    void Start()
    {
        // Atribui o script de Combatant de um objeto pai, no caso, mecha
        combatant = GetComponentInParent<Combatant>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        // Verifica se a colis�o foi realizada com um objeto que possui a classe Combatant
        // Caso tenha, passa para a fun��o TakeDamage do oponente o dano causado e seu tipo
        if(collision.TryGetComponent(out Combatant opponent))
        {
            opponent.TakeDamage(currentDamage, this.tipoDano);
            
            Debug.Log("colis�o" + gameObject.name);
        }
    }

    // Caso tenha sido do tipo r�pido, aplica o QuickDamage, que pega o atributo quickDamage do ScriptableObject do bra�o da inst�ncia que possui esse punho
    public void QuickDamage()
    {
        currentDamage = combatant._rightArmSO.QuickDamage;
        tipoDano = 1;

        Debug.Log("bra�oFraco" + gameObject.name);
    }

    // Caso tenha sido do tipo forte, aplica o StrongDamage, que pega o atributo strongDamage do ScriptableObject do bra�o da inst�ncia que possui esse punho
    public void StrongDamage()
    {
        currentDamage = combatant._leftArmSO.StrongDamage;
        tipoDano = 2;

        Debug.Log("bra�oForte" + gameObject.name);
    }
}
