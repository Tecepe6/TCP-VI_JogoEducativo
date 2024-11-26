using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Brand Scriptable Object")]
public class BrandSO : ScriptableObject
{
    // Status do chassi
    [field:SerializeField] public int MaxLife { get; private set; }
    [field:SerializeField] public int Defense { get; private set; }
    [field:SerializeField] public int DodgeSpeed { get; private set; }
}
