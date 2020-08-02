using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//find and keep track of all bases in level
//report to gameManager if all destroyed therefor game over
public class BaseManager : MonoBehaviour
{
    //all bases destroyed so game over
    public static event Action gameOver;

    private void OnEnable()
    {
        Base.Created += AddBase;
        Base.Destroyed += RemoveBase;
    }

    private List<GameObject> allBase = new List<GameObject>();
    private void AddBase(GameObject newBase)
    {
        if(!allBase.Contains(newBase))
        {
            allBase.Add(newBase);
        }
    }

    private void RemoveBase(GameObject destroyBase)
    {
        if(allBase.Contains(destroyBase))
        {
            allBase.Remove(destroyBase);
            Destroy(destroyBase);

            if(allBase.Count == 0)
            {
                //report game over
                if (gameOver != null)
                {
                    gameOver();
                }
            }
        }
        else
        {
            Debug.LogError("A base that wasn't registered was destroyed, something went wrong!!");
        }
    }
}
