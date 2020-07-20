using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//move toward IK base
public class Enemy : MonoBehaviour
{
    public Vector3 target = Vector3.zero;
    public float speed = 1f;
    private Vector3 direction = Vector3.zero;

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
        Destroy();
    }

    public void Destroy()
    {
        //calls to the dropController and sends it this enemies name???
        //or should it be like level...


        this.gameObject.SetActive(false);

    }
}
