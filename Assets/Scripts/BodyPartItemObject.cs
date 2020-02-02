using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartItemObject : MonoBehaviour {
	public BodyPartItem Item;

	private Color brown = new Color (255 / 255f, 162 / 255f, 11 / 255f, 1);
	private Color yellow = new Color (255 / 255f, 219 / 255f, 0, 1);

	public void Reset () {
		var sr = GetComponent<SpriteRenderer> ();
		sr.sprite = null;
	}

	public void InitSprite (BodyPartItem item) {
		Item = item;
		var sr = GetComponent<SpriteRenderer> ();
		sr.sprite = item.Sprite;
		sr.color = item.Colour == BodyPartColour.Yellow ? yellow : brown;
		transform.localPosition = item.Offset;
		Debug.LogFormat ("Position: {0}, offset: {1}", transform.localPosition, item.Offset);
	}

	public void InitImage (BodyPartItem item) {
		Item = item;
		var im = GetComponent<Image> ();
		im.sprite = item.Sprite;
		im.color = item.Colour == BodyPartColour.Yellow ? yellow : brown;
	}
}