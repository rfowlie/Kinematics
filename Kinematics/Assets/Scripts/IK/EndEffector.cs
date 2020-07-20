using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEffector : MonoBehaviour
{
    public Transform target;
    [Range(0.001f, 0.99f)] public float lerpSpeed = 0.01f;



    private void Update()
    {
        //get vector
        Vector3 endToTarget = target.position - transform.position;
        //rotate
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(endToTarget, transform.up), lerpSpeed)  ;
        //move
        transform.position = Vector3.Lerp(transform.position, target.position, lerpSpeed);
    }
}
