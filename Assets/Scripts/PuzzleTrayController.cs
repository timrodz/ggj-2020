using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrayController : MonoBehaviour {
    [SerializeField]
    private PuzzleGenerator Puzzle = null;

    [SerializeField]
    private List<BodyPartItemObject> TrayGameObjectList = new List<BodyPartItemObject> (4);

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
            InitialiseBodyPart (i, TrayGameObjectList[i], list[i]);
        }
    }

    public void InitialiseBodyPart (int index, BodyPartItemObject obj, BodyPartItem item) {
        obj.Init (item);
        obj.name = string.Format ("body-part-{0}-{1}", index, item.name);
    }
}