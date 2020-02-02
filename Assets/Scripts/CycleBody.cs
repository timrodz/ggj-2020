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
    public GameObject openCloseSendCoffin;

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

        //Hide the tray and portrait
        tray.gameObject.GetComponent<Animator>()?.SetTrigger("Hide");
        portrait.gameObject.GetComponent<Animator>()?.SetTrigger("Hide");
        //Wait
        yield return new WaitForSeconds(1.0f);
        //Close the lid and open the curtains
        openCloseSendCoffin.gameObject.GetComponent<Animator>()?.SetTrigger("Close_Coffin");
        // curtains.gameObject.GetComponent<Animator> ()?.SetTrigger ("Open");
        //Wait
        yield return new WaitForSeconds(1.0f);
        //Send off the Coffin
        openCloseSendCoffin.gameObject.GetComponent<Animator>()?.SetTrigger("Send_Coffin");
        //Wait
        yield return new WaitForSeconds(1.0f);
        //Bring in a new coffin
        coffinRotation.gameObject.GetComponent<Animator> ()?.SetTrigger ("Empty Table");
        // curtains.gameObject.GetComponent<Animator> ()?.SetTrigger ("Close");
        coffinRotation.gameObject.GetComponent<Animator>()?.SetTrigger("Next Coffin");
        yield return new WaitForSeconds (1.0f);
        //wait
        yield return new WaitForSeconds (1.0f);
        //Show the tray and portrait
        tray.gameObject.GetComponent<Animator>()?.SetTrigger("Show");
        portrait.gameObject.GetComponent<Animator>()?.SetTrigger("Show");
        //Open the coffin
        openCloseSendCoffin.gameObject.GetComponent<Animator>()?.SetTrigger("Open_Coffin");

        //In able the buttons
        SetButtonsInteractable(true);

        OnCarryOutFinish?.Invoke ();
    }

}