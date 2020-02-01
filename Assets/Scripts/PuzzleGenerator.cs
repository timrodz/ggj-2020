using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Solution {
    public BodyPartItem RealItem;
    public BodyPartItem FakeItem;

    public Solution (BodyPartItem realItem, BodyPartItem fakeItem) {
        RealItem = realItem;
        FakeItem = fakeItem;
    }

    public override string ToString () {
        return string.Format ("Real Part {0} - Fake Part: {1}", RealItem.name, FakeItem.name);
    }
}

public class PuzzleGenerator : MonoBehaviour {
    [Header ("Body Part Lists")]
    [SerializeField]
    private List<BodyPartItem> RealBodyPartList = new List<BodyPartItem> ();
    [SerializeField]
    private List<BodyPartItem> FakeBodyPartList = new List<BodyPartItem> ();

    [Header ("Tray Part Array")]
    public List<BodyPartItem> TrayPartList = new List<BodyPartItem> ();
    public Solution CurrentSolution = null;
    public Solution PreviousSolution = null;

    private Dictionary<string, int> SolutionDict = new Dictionary<string, int> ();

    private void Start () {
        ClearState ();
    }

    private void Update () {
        if (Input.GetKeyDown (KeyCode.R)) {
            ClearState ();
        }
        if (Input.GetKeyDown (KeyCode.Alpha1)) {
            TestFillTray ();
        }
        if (Input.GetKeyDown (KeyCode.Alpha0)) {
            TestGetSolution ();
        }
    }

    [ContextMenu ("Run: Clear State")]
    public void ClearState () {
        Debug.Log ("Clearing state");
        CurrentSolution = null;
        PreviousSolution = null;
        TrayPartList.Clear ();
        SolutionDict.Clear ();
    }

    public Solution GetSolution (BodyPartType type) {
        // Pick a random real body part
        BodyPartItem realPart = GetRandomBodyPart (RealBodyPartList, type, PreviousSolution != null ? PreviousSolution.RealItem : null);

        // Match a fake body part with it
        BodyPartItem fakePart = FakeBodyPartList.Find ((item) => item.Colour == realPart.Colour && item.Type == realPart.Type && item.Category == realPart.Category);

        if (PreviousSolution != null) {
            PreviousSolution = CurrentSolution;
        }
        RegisterSolution (realPart.name);
        CurrentSolution = new Solution (realPart, fakePart);

        Debug.LogFormat ("New Solution: " + CurrentSolution.ToString ());
        return CurrentSolution;
    }

    public void FillTray (BodyPartType type) {
        TrayPartList.Clear ();
        Solution sol = GetSolution (type);

        if (!sol.FakeItem) {
            Debug.LogError ("Could not find fake part: " + sol.ToString ());
            return;
        }

        BodyPartItem solutionFakeItem = sol.FakeItem;

        TrayPartList.Add (solutionFakeItem);

        var tempFakePartList = new List<BodyPartItem> (FakeBodyPartList);

        // Remove solution fake part
        tempFakePartList.Remove (solutionFakeItem);

        // Equal Type + Category
        var equalTypeCategory = tempFakePartList.FindAll ((item) => item.Type == solutionFakeItem.Type && item.Category == solutionFakeItem.Category && item.Colour != solutionFakeItem.Colour);

        BodyPartItem newItem = equalTypeCategory[Random.Range (0, equalTypeCategory.Count)];
        tempFakePartList.Remove (newItem);
        TrayPartList.Add (newItem);
        Debug.Log ("Equal Type + Category: " + newItem.ToString ());

        // Equal Type + Colour
        var equalTypeColour = tempFakePartList.FindAll ((item) => item.Type == solutionFakeItem.Type && item.Colour == solutionFakeItem.Colour && item.Category != solutionFakeItem.Category);

        newItem = equalTypeColour[Random.Range (0, equalTypeColour.Count)];
        tempFakePartList.Remove (newItem);
        TrayPartList.Add (newItem);
        Debug.Log ("Equal Type + Colour: " + newItem.ToString ());

        // Equal Type (Not equal color + category)
        var equalType = tempFakePartList.FindAll ((item) => item.Type == solutionFakeItem.Type && item.Category != solutionFakeItem.Category && item.Colour != solutionFakeItem.Colour);

        newItem = equalType[Random.Range (0, equalType.Count)];
        tempFakePartList.Remove (newItem);
        TrayPartList.Add (newItem);
        Debug.Log ("Equal Type: " + newItem.ToString ());

        TrayPartList.Shuffle ();
        foreach (BodyPartItem item in TrayPartList) {
            Debug.Log (item.name == solutionFakeItem.name ? "(solution) " + item.ToString () : item.ToString ());
        }
    }

    private BodyPartItem GetRandomBodyPart (List<BodyPartItem> list, BodyPartType type, BodyPartItem toExclude = null) {
        List<BodyPartItem> tempList = new List<BodyPartItem> (list);

        // Remove if item exists and is not the only item in the list
        if (tempList.IndexOf (toExclude) != -1 && tempList.Count > 1) {
            tempList.Remove (toExclude);
        }

        tempList = tempList.FindAll ((item) => item.Type == type);

        BodyPartItem newItem = tempList[Random.Range (0, tempList.Count)];
        Debug.LogFormat ("New random body part: {0}", newItem.name);
        return newItem;
    }

    private void RegisterSolution (string key) {
        if (!SolutionDict.ContainsKey (key)) {
            SolutionDict.Add (key, 0);
        }
        int count = SolutionDict.IncrementCount (key);
        Debug.LogFormat ("Adding solution to dict: {0}, Count: {1}", key, count);
    }

    [ContextMenu ("Run: Get Solution")]
    public void TestGetSolution () {
        GetSolution (BodyPartType.Hair);
    }

    [ContextMenu ("Run: Fill Tray")]
    public void TestFillTray () {
        FillTray (BodyPartType.Hair);
    }
}