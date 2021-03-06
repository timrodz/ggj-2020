﻿using System.Collections;
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
    public GameObject head;
    public GameObject coffinOpen;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    private void OnEnable () {
        PuzzleTrayController.OnScoreCalculated += OnScore;
    }

    private void OnScore (int score) {
        StartCoroutine (CarryOutBodySwapOut (score));
    }

    private void Start () {
        SetButtonsInteractable (false);
        StartCoroutine (CarryBodySwapIn ());
    }
    private void Update () {
        if (coffinOpen.activeInHierarchy) {
            head.SetActive (true);
        } else {
            head.SetActive (false);
        }
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
        AudioManager.Instance.PlayFX ("squelch");
        SetButtonsInteractable (false);
		Debug.LogFormat ("Selected body part: {0}", buttonNumber);
        OnClick?.Invoke (buttonNumber);
	}

    IEnumerator CarryOutBodySwapOut (int score) {
        //Hide the tray and portrait
        tray.gameObject.GetComponent<Animator> ()?.SetTrigger ("Hide");
        portrait.gameObject.GetComponent<Animator> ()?.SetTrigger ("Hide");
        //Wait
        yield return new WaitForSeconds (0.75f);
        if (score == 4) {
			AudioManager.Instance.PlayFX("cheering");
		}
        else if (score == 3) {
            AudioManager.Instance.PlayFX("applause");
        }
        else if (score == 2) {
            AudioManager.Instance.PlayFX("man_cry");
        }
        else {
            AudioManager.Instance.PlayFX("woman_scream");
        }
        yield return new WaitForSeconds (0.75f);
        //Close the coffin
        coffinRotation.gameObject.GetComponent<Animator> ()?.SetTrigger ("Close_Coffin");
        //Wait
        yield return new WaitForSeconds (0.85f);
        AudioManager.Instance.PlayFX("squeak");
        yield return new WaitForSeconds (0.65f);
        //Send off the Coffin
        coffinRotation.gameObject.GetComponent<Animator> ()?.SetTrigger ("Send_Coffin");
		//Wait
		// yield return new WaitForSeconds(0.5f);
		yield return new WaitForSeconds (2.5f);
        OnCarryOutStart?.Invoke ();
        //Bring in a new coffin
        coffinRotation.gameObject.GetComponent<Animator> ()?.SetTrigger ("Empty Table");
        coffinRotation.gameObject.GetComponent<Animator> ()?.SetTrigger ("Next Coffin");
		AudioManager.Instance.PlayFX("belt");
		yield return new WaitForSeconds (2.6f);
        AudioManager.Instance.PlayFX("creak");
        //wait
        yield return new WaitForSeconds (1.0f);
        //Show the tray and portrait
        tray.gameObject.GetComponent<Animator> ()?.SetTrigger ("Show");
        portrait.gameObject.GetComponent<Animator> ()?.SetTrigger ("Show");

		yield return new WaitForSeconds(0.7f);
        AudioManager.Instance.PlayFX("creak");
        
		//In able the buttons
		SetButtonsInteractable (true);
        OnCarryOutFinish?.Invoke ();
    }

}