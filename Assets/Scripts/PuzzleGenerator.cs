using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solution {
    public BodyPartItem RealPart;
    public BodyPartItem FakePart;

    public Solution (BodyPartItem real, BodyPartItem fake) {
        RealPart = real;
        FakePart = fake;
    }

    public override string ToString () {
        return string.Format ("Real Part {0} - Fake Part: {1}", RealPart.name, FakePart.name);
    }
}

public class PuzzleGenerator : MonoBehaviour {
    [Header ("Body Part Lists")]
    [SerializeField]
    private List<BodyPartItem> RealBodyPartList = new List<BodyPartItem> ();
    [SerializeField]
    private List<BodyPartItem> FakeBodyPartList = new List<BodyPartItem> ();

    [Header ("Tray Part Array")]
    public List<BodyPartItem> TrayPartArray = new List<BodyPartItem> ();
    public Solution CurrentSolution = null;
    public Solution PreviousSolution = null;

    private Dictionary<string, int> SolutionDict = new Dictionary<string, int> ();

    [ContextMenu ("Run: Reset State")]
    public void ResetState () {
        Debug.Log ("Clearing state");
        PreviousSolution = null;
        TrayPartArray.Clear ();
        SolutionDict.Clear ();
    }

    [ContextMenu ("Run: Get Solution")]
    public Solution GetSolution (BodyPartType type = BodyPartType.Hair) {
        if (PreviousSolution != null) {
            Debug.LogFormat ("Previous Solution: " + PreviousSolution.ToString ());
        }

        // Pick a random real body part
        BodyPartItem realPart = GetRandomBodyPart (RealBodyPartList, type, PreviousSolution != null ? PreviousSolution.RealPart : null);

        // Match a fake body part with it
        BodyPartItem fakePart = FakeBodyPartList.Find ((item) => item.Colour == realPart.Colour && item.Type == realPart.Type && item.Category == realPart.Category);

        RegisterSolution (realPart.name);
        CurrentSolution = new Solution (realPart, fakePart);
        PreviousSolution = CurrentSolution;

        Debug.LogFormat ("New Solution: " + CurrentSolution.ToString ());
        return CurrentSolution;
    }

    [ContextMenu ("Run: Fill Tray")]
    public void FillTray (BodyPartType type = BodyPartType.Hair) {
        TrayPartArray = new List<BodyPartItem> ();
        Solution sol = GetSolution (type);

        if (!sol.FakePart) {
            Debug.LogError ("Could not find fake part: " + sol.ToString ());
            return;
        }

        BodyPartItem solutionFakePart = sol.FakePart;

        TrayPartArray.Add (solutionFakePart);

        var tempFakePartList = new List<BodyPartItem> (FakeBodyPartList);

        // Remove solution fake part
        tempFakePartList.Remove (solutionFakePart);

        // Equal Type + Category
        var equalTypeCategory = tempFakePartList.FindAll ((item) => item.Type == solutionFakePart.Type && item.Category == solutionFakePart.Category && item.Colour != solutionFakePart.Colour);

        BodyPartItem newPart = equalTypeCategory[Random.Range (0, equalTypeCategory.Count)];
        tempFakePartList.Remove (newPart);
        TrayPartArray.Add (newPart);
        Debug.Log ("Equal Type + Category: " + newPart.ToString ());

        // Equal Type + Colour
        var equalTypeColour = tempFakePartList.FindAll ((item) => item.Type == solutionFakePart.Type && item.Colour == solutionFakePart.Colour && item.Category != solutionFakePart.Category);

        newPart = equalTypeColour[Random.Range (0, equalTypeColour.Count)];
        tempFakePartList.Remove (newPart);
        TrayPartArray.Add (newPart);
        Debug.Log ("Equal Type + Colour: " + newPart.ToString ());

        // Equal Type (Not equal color + category)
        var equalType = tempFakePartList.FindAll ((item) => item.Type == solutionFakePart.Type && item.Category != solutionFakePart.Category && item.Colour != solutionFakePart.Colour);

        newPart = equalType[Random.Range (0, equalType.Count)];
        tempFakePartList.Remove (newPart);
        TrayPartArray.Add (newPart);
        Debug.Log ("Equal Type: " + newPart.ToString ());

        TrayPartArray.Shuffle ();
        foreach (BodyPartItem item in TrayPartArray) {
            Debug.Log (item.name == solutionFakePart.name ? "(solution) " + item.ToString () : item.ToString ());
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
}