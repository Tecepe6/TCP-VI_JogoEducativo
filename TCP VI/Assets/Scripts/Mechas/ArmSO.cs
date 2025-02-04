using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bra�os Scriptable Objects")]

public class ArmSO : ScriptableObject
{
    //added by Pedro if something breaks, blame him
    [field:SerializeField] public string Name {  get; private set; }
    [field:SerializeField] public string Description {  get; private set; }
    
    //This mesh and material separated will probably get deprecated after implementing SkMesh
    [field:SerializeField] public Mesh Mesh {  get; private set; }
    [field:SerializeField] public Material Material {  get; private set; }
    [field:SerializeField] public SkinnedMeshRenderer SkMesh {  get; private set; } // Will only work after adding the logic in MechaDisplay

    // Status do bra�o
    [SerializeField][Range(1, 100)] private int attackSpeed;
    [SerializeField][Range(1, 100)] private int quickDamage;
    [SerializeField][Range(1, 100)] private int strongDamage;
    [SerializeField][Range(1, 100)] private int specialDamage;
    [SerializeField][Range(1, 100)] private int specialRequiredStamina;

    public int AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    public int QuickDamage { get { return quickDamage; } set { quickDamage = value; } }
    public int StrongDamage { get { return strongDamage; } set { strongDamage = value; } }
    public int SpecialRequiredStamina { get {  return specialRequiredStamina; } set {  specialRequiredStamina = value; } }
    public int SpecialDamage { get { return specialDamage; } set { specialDamage = value; } }

    /*
    [field:SerializeField] public int AttackSpeed {  get; private set; }
    [field:SerializeField] public int QuickDamage { get; private set; }
    [field:SerializeField] public int StrongDamage { get; private set; }
    */
}
