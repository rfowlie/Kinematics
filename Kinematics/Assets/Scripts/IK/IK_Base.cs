using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

//the brain
[RequireComponent(typeof(Joint))]
public class IK_Base : MonoBehaviour
{
    [Header("Variables")]
    public GameObject jointPrefab;
    public Radar radar;
    public GameObject target = null;
    public Joint startJoint;
    public Joint endJoint;
    public bool baseMoves = false;  

    [Header("Improvement Values")]
    public float baseLength = 1f;
    public float incrementLength = 0.25f;
    public float baseSpeed = 1f;
    public float incrementSpeed = 0.25f;
    public float baseRadar = 10f;
    public float incrementRadar = 2f;

    [Header("Levels")]
    public int levelJoint = 1;
    public int levelLength = 1;
    public int levelSpeed = 1;
    public int levelRadar = 1;

    
    //get updated targets from the radar subclass
    private void OnEnable()
    {
        radar.NextTarget += SetTarget;
    }
    //if needing to attach joints
    public void CreateJoint()
    {
        levelJoint++;

        GameObject temp = Instantiate(jointPrefab, endJoint.transform);
        temp.transform.localPosition = new Vector3(0, 0, baseLength + incrementLength * levelLength);
        Joint next = temp.GetComponent<Joint>();
        next.parent = endJoint;
        next.lerpSpeed = baseSpeed + levelSpeed * incrementSpeed;
        endJoint.child = next;
        endJoint = next;
    }
    public void RemoveJoint()
    {
        if(levelJoint > 1)
        {
            levelJoint--;

            //remove last joint...        
            Joint parent = endJoint.parent;
            Destroy(endJoint.gameObject);
            endJoint = parent;
            endJoint.child = null;
        }
    }
    public void IncreaseJointLength()
    {
        levelLength++;

        Joint temp = startJoint.child;
        while(temp != null)
        {
            temp.transform.localPosition += new Vector3(0, 0, incrementLength);
            temp = temp.child;
        }
    }
    public void DecreaseLength()
    {
        if(levelLength > 1)
        {
            levelLength--;
            Joint temp = startJoint.child;
            while (temp != null)
            {
                temp.transform.localPosition -= new Vector3(0, 0, incrementLength);
                temp = temp.child;
            }
        }
    }
    public void IncreaseLerpSpeed()
    {
        levelSpeed++;

        Joint temp = startJoint;
        while (temp != null)
        {
            temp.lerpSpeed += incrementSpeed;
            temp = temp.child;
        }
    }
    public void DecreaseLerpSpeed()
    {
        if(levelSpeed > 1)
        {
            levelSpeed--;
            Joint temp = startJoint;
            while (temp != null)
            {
                temp.lerpSpeed -= incrementSpeed;
                temp = temp.child;
            }
        }
    }
    public void IncreaseRadar()
    {
        levelRadar++;
        radar.AdjustRadius(incrementRadar);
    }
    public void DecreaseRadar()
    {
        if(levelRadar > 1)
        {
            levelRadar--;
            radar.AdjustRadius(-incrementRadar);
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
    private void SetTarget(GameObject target)
    {
        this.target = target;
    }
      

    //******************************************
    private void Start()
    {
        //get start attached to this
        startJoint = GetComponent<Joint>();
        endJoint = FindJoints();
            
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
}
