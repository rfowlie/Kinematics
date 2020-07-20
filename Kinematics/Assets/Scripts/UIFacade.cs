using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Control all upgrades through a single UI 
public class UIFacade : MonoBehaviour
{
    public Canvas canvas;
    public IK_Base currentBase;

    private void OnEnable()
    {
        UITrigger.Enter += SetCurrentBase;
        UITrigger.Exit += RemoveBase;
    }

    private void SetCurrentBase(IK_Base newBase)
    {
        currentBase = newBase;
        canvas.gameObject.SetActive(true);
    }

    private void RemoveBase()
    {
        currentBase = null;
        canvas.gameObject.SetActive(false);
    }

    //add all upgrades here and match them with methods from IK_Base
    public void CreateJoint()
    {
        currentBase.CreateJoint();
    }

    public void IncreaseJointLength()
    {
        currentBase.IncreaseJointLength(0.25f);
    }
    
    public void IncreaseLerpSpeed()
    {
        currentBase.IncreaseLerpSpeed(0.25f);
    }
}
