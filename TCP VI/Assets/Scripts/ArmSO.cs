using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bra�os Scriptable Objects")]

public class ArmSO : ScriptableObject
{
    // Status do bra�o
    [field:SerializeField] public int AttackSpeed {  get; private set; }
    [field:SerializeField] public int QuickDamage { get; private set; }
    [field:SerializeField] public int StrongDamage { get; private set; }
}
