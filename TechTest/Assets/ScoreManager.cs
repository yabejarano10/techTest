﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI text;
    public TextMeshProUGUI specialtext;
    int score;
    int specialScore;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void ChangeScore(int value)
    {
        score += value;
        text.text = "X" + score.ToString();
    }

    public void ChangeSpecialScore(int value)
    {
        specialScore += value;
        specialtext.text = "X" + specialScore.ToString();
    }
}
