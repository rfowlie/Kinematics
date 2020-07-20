using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//keep track of all enemies that come within trigger
[RequireComponent(typeof(SphereCollider))]
public class Radar : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public SphereCollider sc = null;
    public LayerMask enemyLayer;
    private GameObject currentEnemy;
    public float awarnessRadius = 10f;

    public event Action<GameObject> NextTarget;


    private void OnEnable()
    {
        Enemy.Remove += CheckList;
    }

    private void Start()
    {
        sc = GetComponent<SphereCollider>();
        sc.isTrigger = true;
        sc.radius = awarnessRadius;
    }


    private void CheckList(GameObject obj)
    {
        if (enemies.Contains(obj))
        {
            enemies.Remove(obj);
        }

        if (currentEnemy == obj)
        {
            currentEnemy = FindNewTarget();
            NextTarget(currentEnemy);
        }
    }

    private GameObject FindNewTarget()
    {
        foreach (GameObject e in enemies)
        {
            if (e.activeSelf)
            {
                return e;
            }
        }

        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMaskEX.IsInLayerMask(other.gameObject.layer, enemyLayer))
        {
            //Debug.Log("<color=red>Enemy entered Radar</color>");
            enemies.Add(other.gameObject);

            if (currentEnemy == null)
            {
                currentEnemy = FindNewTarget();
                NextTarget(currentEnemy);
            }
        }
    }


    //Debug
    public Color gizmosColour = Color.white;
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColour;
        Gizmos.DrawWireSphere(transform.position, sc.radius);
    }
}
