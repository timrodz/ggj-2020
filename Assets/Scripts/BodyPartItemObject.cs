using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartItemObject : MonoBehaviour {
    public BodyPartItem Item;

    private SpriteRenderer SpriteRenderer;

    private void Awake () {
		SpriteRenderer = GetComponent<SpriteRenderer>();
	}

    public void Init (BodyPartItem item) {
        Item = item;
		SpriteRenderer.sprite = item.Sprite;
		transform.position = transform.position;
	}
}