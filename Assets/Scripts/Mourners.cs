using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Mourners : MonoBehaviour {

    public List<MournerMoods> MournerObjects = new List<MournerMoods> ();

    public void DeactivateMourners () {
        for (int i = 0; i < MournerObjects.Count; ++i) {
            MournerObjects[i].transform.DOScale (Vector3.zero, 1.0f).SetEase (Ease.InBack);
        }
    }

    private void Start () {
        for (int i = 0; i < MournerObjects.Count; ++i) {
            MournerObjects[i].gameObject.SetActive (false);
        }
    }

    private void OnEnable () {
        PuzzleTrayController.OnScoreCalculated += MakeUsHappy;
        CycleBody.OnCarryOutStart += DeactivateMourners;
    }

    private void OnDisable () {
        PuzzleTrayController.OnScoreCalculated -= MakeUsHappy;
        CycleBody.OnCarryOutStart -= DeactivateMourners;
    }

    private void MakeUsHappy (int score) {
        StartCoroutine (Animate (score));
    }

    IEnumerator Animate (int score) {
        yield return new WaitForSeconds (1.0f);
        int tempScore = score;

        for (int i = 0; i < MournerObjects.Count; ++i) {
            MournerObjects[i].transform.localScale = Vector3.zero;
            MournerObjects[i].gameObject.SetActive (true);
            MournerObjects[i].happyFace.SetActive (false);
            MournerObjects[i].sadFace.SetActive (false);
            if (tempScore > 0) {
                MournerObjects[i].happyFace.SetActive (true);
            } else {
                MournerObjects[i].sadFace.SetActive (true);
            }
            MournerObjects[i].transform.DOScale (Vector3.one, 1.0f).SetEase (Ease.OutBack);
            tempScore--;
        }

        MournerObjects.Shuffle ();
    }
}