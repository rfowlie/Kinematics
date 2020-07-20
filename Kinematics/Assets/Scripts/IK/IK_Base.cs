using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

//the brain
[RequireComponent(typeof(Joint))]
public class IK_Base : MonoBehaviour
{
    public TargetPlayer targetPlayer;
    public Vector3 target = Vector3.zero;
    //public Transform target;
    public Joint startJoint;
    public Joint endJoint;
    public bool baseMoves = false;
    public float marginOfError = 0.1f;

    private void Start()
    {
        //get start attached to this
        startJoint = GetComponent<Joint>();

        //setup all joints
        Joint temp = startJoint;
        while(true)
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

        //get endJoint, need for calculations
        endJoint = temp;        
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
    }

    private void FixedUpdate()
    {
        if(target == Vector3.zero)
        {
            return;
        }

        //update target of endEffector
        endJoint.target = target;

        //don't run when super close to target
        if ((endJoint.target - endJoint.transform.position).magnitude < marginOfError)
        {
            if(targetPlayer.points.Count != 0)
            {
                targetPlayer.points.Dequeue();
                if(targetPlayer.points.Count > 0)
                {
                    target = targetPlayer.points.Peek();
                }                
            }
            else
            {
                target = Vector3.zero;
            }

            return;
        }

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

    private void CheckIfNull()
    {
        if(target == Vector3.zero)
        {
            target = targetPlayer.points.Peek();
        }
    }
    
    //sub to target player event to know if new targets available when 
    //there are none 
    private void OnEnable()
    {
        targetPlayer.click += CheckIfNull;
    }
    private void OnDisable()
    {
        targetPlayer.click -= CheckIfNull;
    }
}
