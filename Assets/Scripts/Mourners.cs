using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mourners : MonoBehaviour
{

    public List<MournerMoods> MournerObjects = new List<MournerMoods>();


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {

    }


    private void ActivateMourners()
    {
        for (int i = 0; i < MournerObjects.Count; ++i)
        {
            MournerObjects[i].gameObject.SetActive(true);
        }
    }

    public void DeactivateMourners()
    {
        for (int i = 0; i < MournerObjects.Count; ++i)
        {
            MournerObjects[i].gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        PuzzleTrayController.OnScoreCalculated += MakeUsHappy;
    }

    private void MakeUsHappy(int score)
    {
        //MournerObjects.Shuffle();
        int tempScore = score;

        for (int i = 0; i < MournerObjects.Count; ++i)
        {
            MournerObjects[i].gameObject.SetActive(true);
            MournerObjects[i].happyFace.SetActive(false);
            MournerObjects[i].sadFace.SetActive(false);
            if (tempScore > 0)
            {
                MournerObjects[i].happyFace.SetActive(true);
            }
            else
            {
                MournerObjects[i].sadFace.SetActive(true);
            }
            tempScore--;
        }
    }

}
