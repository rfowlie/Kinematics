using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class OneJoint : MonoBehaviour
{
    [SerializeField] private Transform target;


    private Transform pivot;
    public Transform point;

    public float maxLength = 4f;
    public float lerpSpeed = 0.1f;

    private void Start()
    {
        pivot = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //get vector from pivot to target
        Vector3 pivotToTarget = target.position - pivot.position;

        Vector3 goToPoint = Vector3.zero;
        //get point that point needs to be at
        if (pivotToTarget.magnitude >= maxLength)
        {
            goToPoint = pivotToTarget.normalized * maxLength;
        }
        else
        {            
            goToPoint = pivotToTarget.normalized * pivotToTarget.magnitude;
        }        

        //not pointing in the same direction
        if(point.position != goToPoint)
        {
            Debug.Log("Not pointing in same direction");
            //try simple lerp???
            point.transform.position = Vector3.Lerp(point.transform.position, goToPoint, lerpSpeed);
        }
    }
}
