using UnityEngine;
[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Characters/Character Data")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    [Header("Directional Animation Sets")]
    public DirectionalAnimationSet up;
    public DirectionalAnimationSet down;
    public DirectionalAnimationSet left;
    public DirectionalAnimationSet right;
}