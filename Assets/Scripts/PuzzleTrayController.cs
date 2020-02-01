﻿using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrayController : MonoBehaviour {
    [SerializeField]
    private PuzzleGenerator Puzzle = null;
    
    [Header("Objects")]
    [SerializeField]
    private List<BodyPartItemObject> TrayGameObjectList = new List<BodyPartItemObject> (4);

	[SerializeField]
	private BodyPartItemObject ObjectToMatch;
    
    [SerializeField]
	private BodyPartItemObject CorpseObject;
    
    private void Awake() {
		CycleBody.OnClick += OnBodyPartSelected;
		CycleBody.OnCarryOutFinish += OnReset;
	}
    
    private void OnBodyPartSelected(int buttonNumber) {
		CorpseObject.InitSprite(TrayGameObjectList[buttonNumber].Item);
	}
    
    private void OnReset() {
        Init (BodyPartType.Hair);
    }

	private void Update () {
        if (Input.GetKeyDown (KeyCode.P)) {
            Init (BodyPartType.Hair);
        }
    }

    public void Init (BodyPartType type) {
        Puzzle.ClearState ();
        Puzzle.FillTray (type);
        var list = Puzzle.TrayPartList;

        for (int i = 0; i < list.Count; ++i) {
            InitialiseBodyPartImage (i, TrayGameObjectList[i], list[i]);
        }

		CorpseObject.Reset();
		ObjectToMatch.InitSprite(Puzzle.CurrentSolution.RealItem);
	}

    public void InitialiseBodyPartSprite (int index, BodyPartItemObject obj, BodyPartItem item) {
        obj.InitSprite (item);
        obj.name = string.Format ("sprite-body-part-{0}-{1}", index, item.name);
    }

    public void InitialiseBodyPartImage (int index, BodyPartItemObject obj, BodyPartItem item) {
        obj.InitImage (item);
        obj.name = string.Format ("image-body-part-{0}-{1}", index, item.name);
    }
}