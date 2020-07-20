using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base of FK, holds target and talks to all segments
[RequireComponent(typeof(Segment))]
public class FK_Base : MonoBehaviour
{
    public Segment segStart = null;
    private void Start()
    {
        segStart = GetComponent<Segment>();
        //this should setup all segments attached
        segStart.Setup();
    }
}
