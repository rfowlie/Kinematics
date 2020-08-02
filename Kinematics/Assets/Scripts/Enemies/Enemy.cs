using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void Damage();
}

//move toward IK base
public class Enemy : MonoBehaviour, IDamagable
{
    public Vector3 target = Vector3.zero;
    public float speed = 1f;
    private Vector3 direction = Vector3.zero;
    public int pointValue = 10;
    public LayerMask canDamage;

    private void Update()
    {
        direction = (target - transform.position).normalized;
    }
    private void FixedUpdate()
    {

        transform.position += direction * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMaskEX.IsInLayerMask(collision.gameObject.layer, canDamage))
        {
            PlayerDestroyed(pointValue);
        }

        Recycle(gameObject);
    }
    
    public void Damage()
    {
        PlayerDestroyed(pointValue);
        Recycle(gameObject);
    }

    //event
    public static event Action<int> PlayerDestroyed;
    public static event Action<GameObject> Recycle;
}
