using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Control all upgrades through a single UI 
public class UIFacade : MonoBehaviour
{
    public Canvas canvas;
    public IK_Base currentBase;

    private void Start()
    {
        //make sure that UI is null at first
        RemoveBase();
    }


    private void OnEnable()
    {
        UITrigger.Enter += SetCurrentBase;
        UITrigger.Exit += RemoveBase;
        Score.Reevaluate += GetScore;
    }

    private void SetCurrentBase(IK_Base newBase)
    {
        currentBase = newBase;
        canvas.gameObject.SetActive(true);
        SetCosts();
    }

    private void RemoveBase()
    {
        currentBase = null;
        canvas.gameObject.SetActive(false);
    }

    private int score = 0;
    private void GetScore(int score)
    {
        this.score = score;
        if(currentBase != null)
        {
            SetUI();
        }
    }

    //UI Buttons
    private void SetUI()
    {
        canCreateJoint = score > costJoint * currentBase.levelJoint ? true : false;
        canIncreaseLength = score > costLength * currentBase.levelLength ? true : false;
        canIncreaseLerpSpeed = score > costSpeed * currentBase.levelSpeed ? true : false;
        canIncreaseRadar = score > costRadar * currentBase.levelRadar ? true : false;
    }

    private void SetCosts()
    {
        currentCostJoint = costJoint * currentBase.levelJoint;
        currentCostLength = costLength * currentBase.levelLength;
        currentCostSpeed = costSpeed * currentBase.levelSpeed;
        currentCostRadar = costRadar * currentBase.levelRadar;
    }

    public int costJoint = 100;
    public int costLength = 80;
    public int costSpeed = 150;
    public int costRadar = 50;
    public bool canCreateJoint = false;
    public int currentCostJoint = 0;
    public bool canIncreaseLength = false;
    public int currentCostLength = 0;
    public bool canIncreaseLerpSpeed = false;
    public int currentCostSpeed = 0;
    public bool canIncreaseRadar = false;
    public int currentCostRadar = 0;

    public static event Action<int> Buy;

    //add all upgrades here and match them with methods from IK_Base
    public void CreateJoint()
    {
        if (canCreateJoint)
        {
            Buy(costJoint * currentBase.levelJoint);
            currentBase.CreateJoint();
            currentCostJoint = costJoint * currentBase.levelJoint;
        }
    }
    public void IncreaseJointLength()
    {
        if (canIncreaseLength)
        {
            Buy(costLength * currentBase.levelLength);
            currentBase.IncreaseJointLength();
            currentCostLength = costLength * currentBase.levelLength;
        }
    }    
    public void IncreaseLerpSpeed()
    {
        if (canIncreaseLerpSpeed)
        {
            Buy(costSpeed * currentBase.levelSpeed);
        	currentBase.IncreaseLerpSpeed();
            currentCostSpeed = costSpeed * currentBase.levelSpeed;
        }
    }
    public void IncreaseRadar()
    {
        if(canIncreaseRadar)
        {
            Buy(costRadar * currentBase.levelRadar);
            currentBase.IncreaseRadar();
            currentCostRadar = costRadar * currentBase.levelRadar;
        }
    }
}
