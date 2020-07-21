using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//move toward IK base
public class Enemy : MonoBehaviour
{
    public Vector3 target = Vector3.zero;
    public float speed = 1f;
    private Vector3 direction = Vector3.zero;
    public int pointValue = 10;

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
        Deactivate();
    }

    public void Deactivate()
    {
        Death(pointValue);
        Remove(gameObject);
    }

    //event
    public static event Action<int> Death;
    public static event Action<GameObject> Remove;
}
