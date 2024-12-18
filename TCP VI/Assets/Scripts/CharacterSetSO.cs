using AYellowpaper.SerializedCollections;
using UnityEngine;


[CreateAssetMenu(menuName = "CharacterSet")]
public class CharacterSetSO : ScriptableObject
{
    [field:SerializeField] public string CharacterName { get; private set; }
    
    [SerializedDictionary("Expresion","Image")] public SerializedDictionary<string, Sprite> Expressions;
}
