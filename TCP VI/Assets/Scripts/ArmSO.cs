using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Braços Scriptable Objects")]

public class ArmSO : ScriptableObject
{
    // Status do braço
    [field:SerializeField] public int AttackSpeed {  get; private set; }
    [field:SerializeField] public int WeakDamage { get; private set; }
    [field:SerializeField] public int StrongDamage { get; private set; }
}
