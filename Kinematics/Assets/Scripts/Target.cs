using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    public Color gizmosColour = Color.cyan;
    public float gimzoRadius = 1f;

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = gizmosColour;
        Gizmos.DrawSphere(transform.position, gimzoRadius);
    }
}