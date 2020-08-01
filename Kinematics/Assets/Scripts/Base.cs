using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//the base the player has to protect
[RequireComponent(typeof(Collider))]
public class Base : MonoBehaviour
{
    //health
    public int healthMax;
    public int healthCurrent;
    public LayerMask canDamage;

    public static event Action<GameObject> Created;
    public static event Action<GameObject> Destroyed;

    private void OnCollisionEnter(Collision collision)
    {
        if(LayerMaskEX.IsInLayerMask(collision.gameObject.layer, canDamage))
        {
            healthCurrent--;

            if(healthCurrent <= 0)
            {
                if(Destroyed != null)
                {
                    Destroyed(gameObject);
                }                
            }
        }
    }

    private void Start()
    {
        //set starting health
        healthCurrent = healthMax;

        if(Created != null)
        {
            Created(gameObject);
        }
    }
}
