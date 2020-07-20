using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


//first try at FK
//put on each part of the FK
public class Segment : MonoBehaviour
{
    //transform stores its position
    public float length = 3f;
    public int angleSelf = 0;
    private int angleParent = 0;
    public Segment childSegment = null;

    public void Setup()
    {
        //check if this has a child with segment
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Segment>())
                {
                    childSegment = transform.GetChild(i).GetComponent<Segment>();
                    childSegment.transform.position = DeterminePoint();
                    childSegment.angleParent = angleParent + angleSelf;
                    childSegment.Setup();
                }
            }
        }
    }

    private Vector3 DeterminePoint()
    {
        //calculate end point from length and angle
        float dx = length * Mathf.Cos(Mathf.Deg2Rad * (angleSelf + angleParent));
        float dy = length * Mathf.Sin(Mathf.Deg2Rad * (angleSelf + angleParent));
        return transform.position + new Vector3(dx, dy, 0f);
    }

    private void Update()
    {
        if(childSegment != null)
        {
            childSegment.transform.position = DeterminePoint();
            childSegment.angleParent = angleParent + angleSelf;
        }
    }

    //debug
    private void OnDrawGizmos()
    {                        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.25f);
        if(childSegment != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, childSegment.transform.position);
        }
    }
}
