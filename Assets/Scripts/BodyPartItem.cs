using UnityEngine;

[CreateAssetMenu (fileName = "BodyPartItem", menuName = "GGJ/Body Part Item", order = 0)]
public class BodyPartItem : ScriptableObject {
    [Header("Is it a real body part? (Used for giving reference to a player)")]
    public bool IsReal = false;
    [Header("What body part ties to this object?")]
    public BodyPartType Type = BodyPartType.Hair;
    [Header("How would you describe this object?")]
	public BodyPartCategory Category = BodyPartCategory.Dreadlocks;
    [Header("What colour is this object?")]
    public BodyPartColour Colour = BodyPartColour.Brown;
    [Header("What sprite is tied to this object?")]
    public Sprite Sprite = null;
	// public Vector2 Position = Vector2.zero;
	// public Vector2 Rotation = Vector2.zero;
    
    public override string ToString() {
		return string.Format("BodyPartItem {0} = Type: {1} - Category: {2}, Colour: {3}", name, Type, Category, Colour);
	}
}
