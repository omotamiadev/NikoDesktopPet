using UnityEngine;
[System.Serializable]
public class DirectionalAnimationSet
{
    [Tooltip("Idle sprite for this direction")]
    public Sprite idle;

    [Tooltip("Walking sprites in order: walk1, walk2, walk3")]
    public Sprite[] walkCycle = new Sprite[3]; 
}