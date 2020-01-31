using UnityEngine;

[CreateAssetMenu (fileName = "BodyPartItem", menuName = "GGJ/Body Part Item", order = 0)]
public class BodyPartItem : ScriptableObject {
    [Header("What body part ties to this object?")]
    public BodyPartType Type = BodyPartType.Hair;
    [Header("How would you describe this object?")]
	public BodyPartCategory Category = BodyPartCategory.Dreadlocks;
    [Header("What colour is this object?")]
    public BodyPartColour Colour = BodyPartColour.Black;
    [Header("What sprite is tied to this object?")]
    public Sprite Sprite = null;
	public Vector2 Position = Vector2.zero;
	public Vector2 Rotation = Vector2.zero;
}
