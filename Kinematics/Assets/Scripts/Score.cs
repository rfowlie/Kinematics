using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using System;
//using UnityEditor.VersionControl;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI text = null;
    public int score = 0;


    private void OnEnable()
    {
        Enemy.PlayerDestroyed += IncrementScore;
        UIFacade.Buy += DecrementScore;
    }

    private void Start()
    {
        score = 0;
        text.text = score.ToString();
    }

    public static event Action<int> Reevaluate;

    //get points
    public void IncrementScore(int amount)
    {
        score += amount;
        text.text = score.ToString();
        if(Reevaluate != null)
        {
            Reevaluate(score);
        }
    }
    //spend points
    public void DecrementScore(int amount)
    {
        //potentially do checks here
        score -= amount;
        text.text = score.ToString();
        if (Reevaluate != null)
        {
            Reevaluate(score);
        }
    }
}
