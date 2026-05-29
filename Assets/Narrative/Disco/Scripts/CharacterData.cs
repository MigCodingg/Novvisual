using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Disco/CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] public string characterName;
    [SerializeField] public Sprite portrait;
}
