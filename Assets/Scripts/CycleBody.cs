using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CycleBody : MonoBehaviour {
    public static System.Action<int> OnClick;
    public static System.Action OnCarryOutStart;
    public static System.Action OnCarryOutFinish;

    public GameObject tray;
    public GameObject portrait;
    public GameObject curtains;
    public GameObject coffinRotation;
    public GameObject closedCoffin;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    private void OnEnable () {
		AudioManager.Instance.PlayFX("belt");
		PuzzleTrayController.OnScoreCalculated += OnScore;
    }

    private void OnScore (int score) {
        StartCoroutine (CarryOutBodySwapOut ());
    }

    private void Start () {
        SetButtonsInteractable (false);
        StartCoroutine (CarryBodySwapIn ());
    }

    IEnumerator CarryBodySwapIn () {
        yield return new WaitForSeconds (1.5f);
        SetButtonsInteractable (true);
        OnCarryOutFinish?.Invoke ();
    }

    private void SetButtonsInteractable (bool value) {
        button1.interactable = value;
        button2.interactable = value;
        button3.interactable = value;
        button4.interactable = value;
    }

    public void SelectBodyPart (int buttonNumber) {
        Debug.LogFormat ("Selected body part: {0}", buttonNumber);
        SetButtonsInteractable (false);
        OnClick?.Invoke (buttonNumber);
    }

    IEnumerator CarryOutBodySwapOut () {
        OnCarryOutStart?.Invoke ();

        // TODO: Play score animation

        yield return new WaitForSeconds (1f);

        tray.gameObject.GetComponent<Animator> ()?.SetTrigger ("Hide");
        portrait.gameObject.GetComponent<Animator> ()?.SetTrigger ("Hide");
        // curtains.gameObject.GetComponent<Animator> ()?.SetTrigger ("Open");
        coffinRotation.gameObject.GetComponent<Animator> ()?.SetTrigger ("Empty Table");

        yield return new WaitForSeconds (1.5f);

        //Replace the animated coffin with the closed coffin
        //closedCoffin.SetActive (true);

        tray.gameObject.GetComponent<Animator> ()?.SetTrigger ("Show");
        portrait.gameObject.GetComponent<Animator> ()?.SetTrigger ("Show");
        // curtains.gameObject.GetComponent<Animator> ()?.SetTrigger ("Close");
        coffinRotation.gameObject.GetComponent<Animator> ()?.SetTrigger ("Next Coffin");

        yield return new WaitForSeconds (1.0f);

        SetButtonsInteractable (true);

        OnCarryOutFinish?.Invoke ();
    }

}