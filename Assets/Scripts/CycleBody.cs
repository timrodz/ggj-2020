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

    // If you press any one assign that Part to the slot on the corpse
    //Wait
    //Hide the Tray
    //Wait
    //Close the coffin
    //Roll the coffin into the Viewing room
    //Wait
    //Open the Coffin
    //REVEAL FX
    //Wait
    //Award points
    //Wait
    //coffin gets kicked out
    //Hide viewing room
    //Reset viewing room (Ask about how to call the new answer)
    //Reveal the viewing room
    //Roll the belt to put the body in the centre
    //Open the coffin
    //Filled the tray with the options
    //show the tray

    //Use this to assign the chosen part to the corpse

    private void Start() {
		SetButtonsInteractable(false);
		StartCoroutine(CarryBodySwapIn());
	}
    
    IEnumerator CarryBodySwapIn() {
        yield return new WaitForSeconds (1.5f);
		SetButtonsInteractable(true);
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
        StartCoroutine (CarryOutBodySwapOut ());
    }

    IEnumerator CarryOutBodySwapOut () {
        OnCarryOutStart?.Invoke ();

        // TODO: Play animation

        //yield return new WaitForSeconds (1f);

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