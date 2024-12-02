using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Brand Scriptable Object")]
public class BrandSO : ScriptableObject
{
    //added by Pedro if something breaks, blame him
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public string Description { get; private set; }
    
    //This mesh and material separated will probably get deprecated after implementing SkMesh
    [field:SerializeField] public Mesh Mesh { get; private set; }
    [field:SerializeField] public Material Material { get; private set; }
    [field:SerializeField] public SkinnedMeshRenderer SkMesh { get; private set; } // Will only work after adding the logic in MechaDisplay
    
    // Status do chassi
    [field:SerializeField] public int MaxLife { get; private set; }
    [field:SerializeField] public int Defense { get; private set; }
    [field:SerializeField] public int DodgeSpeed { get; private set; }
}
