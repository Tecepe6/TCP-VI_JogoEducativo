using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Brand Scriptable Object")]
public class BrandSO : ScriptableObject
{
    //added by Pedro if something breaks, blame him
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }

    //This mesh and material separated will probably get deprecated after implementing SkMesh
    [field: SerializeField] public Mesh Mesh { get; private set; }
    [field: SerializeField] public Material Material { get; private set; }
    [field: SerializeField] public SkinnedMeshRenderer SkMesh { get; private set; } // Will only work after adding the logic in MechaDisplay

    // Status do chassi
    [SerializeField][Range(1, 100)] public int maxLife;
    [SerializeField][Range(1, 100)] public int maxStamina;
    [SerializeField][Range(1, 100)] private int staminaRecoveryRate;
    [SerializeField][Range(1, 100)] private int staminaRegenDelay;

    /*
    [SerializeField][Range(1, 100)] private int quickPunchRequiredStamina;
    [SerializeField][Range(1, 100)] private int strongPunchRequiredStamina;
    [SerializeField][Range(1, 100)] private int specialPunchRequiredStamina;
    */

    [SerializeField][Range(1, 100)] private int dodgeRequiredStamina;
    [SerializeField] [Range(1, 100)] private int defense;
    [SerializeField] [Range(1, 100)] private int dodgeSpeed;

    // Propriedades públicas para acessar os campos privados de forma segura
    public int MaxLife { get { return maxLife; } private set { maxLife = value; } }
    public int MaxStamina { get { return maxStamina; } private set { maxStamina = value; } }
    public int StaminaRecoveryRate { get { return staminaRecoveryRate; } private set { staminaRecoveryRate = value; } }
    public int StaminaRegenDelay { get { return staminaRegenDelay; } private set { staminaRegenDelay = value; } }

    /*
    public int QuickPunchRequiredStamina { get { return quickPunchRequiredStamina; } private set { quickPunchRequiredStamina = value; } }
    public int StrongPunchRequiredStamina { get { return strongPunchRequiredStamina; } private set { strongPunchRequiredStamina = value; } }
    public int SpecialPunchRequiredStamina { get { return specialPunchRequiredStamina; } private set { specialPunchRequiredStamina = value; } }
    */

    public int DodgeRequiredStamina { get { return dodgeRequiredStamina; } private set { dodgeRequiredStamina = value; } }
    public int Defense { get { return defense; } private set { defense = value; } }
    public int DodgeSpeed { get { return dodgeSpeed; } private set { dodgeSpeed = value; } }
}
