using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartItemObject : MonoBehaviour {
	public BodyPartItem Item;

	private Color brown = new Color (205f / 255f, 133f / 255f, 63f / 255f, 1);

	public void Reset () {
		var sr = GetComponent<SpriteRenderer> ();
		sr.sprite = null;
	}

	public void InitSprite (BodyPartItem item) {
		Item = item;
		var sr = GetComponent<SpriteRenderer> ();
		sr.sprite = item.Sprite;
		sr.color = item.Colour == BodyPartColour.Yellow ? Color.yellow : brown;
		transform.position = item.Offset;
	}

	public void InitImage (BodyPartItem item) {
		Item = item;
		var im = GetComponent<Image> ();
		im.enabled = false;
		im.sprite = item.Sprite;
		im.color = item.Colour == BodyPartColour.Yellow ? Color.yellow : brown;
		im.enabled = true;
		transform.position = transform.position;
	}
}