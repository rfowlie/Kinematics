using Boo.Lang;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

//the brain
[RequireComponent(typeof(Joint))]
public class IK_Base : MonoBehaviour
{
    public GameObject jointPrefab;
    public GameObject target = null;
    public Joint startJoint;
    public Joint endJoint;
    public bool baseMoves = false;
    public int numberOfJoints = 0;
    


    private void Start()
    {
        //get start attached to this
        startJoint = GetComponent<Joint>();
        endJoint = FindJoints();
            
    }

    //if needing to attach joints
    public void CreateJoint()
    {
        GameObject temp = Instantiate(jointPrefab, endJoint.transform);
        temp.transform.localPosition = new Vector3(0, 0, 3);
        temp.GetComponent<Joint>().parent = endJoint;
        endJoint.child = temp.GetComponent<Joint>();
        endJoint = endJoint.child;
    }

    public void IncreaseJointLength(float add)
    {
        Joint temp = startJoint.child;
        while(temp != null)
        {
            temp.transform.localPosition += new Vector3(0, 0, add);
            temp = temp.child;
        }
    }

    public void IncreaseLerpSpeed(float add)
    {
        Joint temp = startJoint;
        while (temp != null)
        {
            temp.lerpSpeed += add;
            temp = temp.child;
        }
    }

    //if already attached
    private Joint FindJoints()
    {
        //setup all joints
        Joint temp = startJoint;
        while (true)
        {
            for (int i = 0; i < temp.transform.childCount; i++)
            {
                if (temp.transform.GetChild(i).GetComponent<Joint>())
                {
                    //set child and parent
                    Joint next = temp.transform.GetChild(i).GetComponent<Joint>();
                    temp.child = next;
                    next.parent = temp;
                    break;
                }
            }

            if (temp.child == null) { break; }
            else { temp = temp.child; }
        }

        return temp;
    }

    private void Update()
    {
        //toggle gizmo's
        if(Input.GetKeyUp(KeyCode.G))
        {
            Joint temp = startJoint;
            while(temp.child != null)
            {
                temp.gizmosOn = !temp.gizmosOn;
                temp = temp.child;
            }
        }
        if(Input.GetKeyUp(KeyCode.C))
        {
            CreateJoint();
        }
    }

    private void FixedUpdate()
    {
        if(target == null || startJoint == endJoint)
        {
            return;
        }

        //update target of endEffector
        endJoint.target = target.transform.position;

        Joint temp = endJoint.parent;
        while (temp != null)
        {
            //call all calculate functions on current joint
            temp.CalcTarget();
            temp.CalcRotation();
            temp = temp.parent;
        }

        //move base...
        if(baseMoves)
        {
            startJoint.transform.position = Vector3.Lerp(startJoint.transform.position,
                                                         startJoint.target, 
                                                         startJoint.lerpSpeed * Time.fixedDeltaTime);        
        }
    }

    private void SetTarget(GameObject target)
    {
        this.target = target;
    }
    
    //get updated targets from the radar subclass
    private void OnEnable()
    {
        radar.NextTarget += SetTarget;
    }

    public Radar radar;
}
