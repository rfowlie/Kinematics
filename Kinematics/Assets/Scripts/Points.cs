using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class Points : MonoBehaviour
{
    public TextMeshProUGUI text = null;
    public int score = 0;


    private void OnEnable()
    {
        Enemy.Death += IncrementScore;
    }

    private void Start()
    {
        score = 0;
        text.text = score.ToString();
    }

    //get points
    public void IncrementScore(int amount)
    {
        score += amount;
        text.text = score.ToString();
    }
    //spend points
    public void DecrementScore(int amount)
    {
        //potentially do checks here
        score += amount;
        text.text = score.ToString();
    }
}
