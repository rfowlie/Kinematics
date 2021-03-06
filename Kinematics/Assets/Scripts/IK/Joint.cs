﻿using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

//TODO
//Make this the base class for various different joint types
//probably have to put the actual moving code in joints not IK_Base
//so that when there are restrictions and shit it can just blindly call the changes



//requires setup in the editor to ensure proper retention of dimensions
public class Joint : MonoBehaviour
{
    [Header("Variables")]
    public Joint parent;
    public Joint child;
    public Vector3 target;
    public float lerpSpeed = 1f;

    [Header("Gizmos")]
    public bool gizmosOn = false;
    public Color gizmoColor = Color.white;
    public float gizmoSize = 0.2f;



    //determine this joints target position from the target of its child
    public void CalcTarget()
    {
        target = (transform.position - child.target).normalized * child.transform.localPosition.magnitude + child.target;
    }

    //determine the rotation required to face child target
    public void CalcRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              Quaternion.LookRotation(child.target - transform.position, transform.forward),
                                              lerpSpeed * Time.fixedDeltaTime);
    }   

    //debug
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        if(child != null)
        {
            Gizmos.DrawLine(transform.position, child.transform.position);
        }
        if(gizmosOn)
        {
            Gizmos.DrawCube(target, new Vector3(gizmoSize, gizmoSize, gizmoSize));
        }        
    }
}
