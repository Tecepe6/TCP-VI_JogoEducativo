using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class ScriptableViewer
{
    [Header("STATUS BRAÇO DIREITO")]
    ArmSO _armSO;

    [field: SerializeField] public int AttackSpeed => _armSO.AttackSpeed;
    [field: SerializeField] public int WeakDamage => _armSO.WeakDamage;
    [field: SerializeField] public int StrongDamage => _armSO.StrongDamage;

    public ScriptableViewer(ArmSO armSO)
    {
        _armSO = armSO;
    }
}
