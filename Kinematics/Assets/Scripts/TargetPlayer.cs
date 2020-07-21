using Boo.Lang;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.U2D;

public class TargetPlayer : Target
{
    InputManager ip = null;

    public float speed = 1.0f;
    private float h = 0f;
    private float v = 0f;
    private float angle = 0f;

    public GameObject bulletPrefab;
    public GameObject minePrefab;

    [Tooltip("Number of bullets per second")]
    public int fireRate = 1;
    public float fireSpeed = 0;

    private void Start()
    {
        ip = InputManager.Instance;
        fireSpeed = 1f / fireRate;
    }

    private void Update()
    {
        //movement
        h = Input.GetAxis(ip.HorizontalAxis);
        v = Input.GetAxis(ip.VerticalAxis);

        //rotation
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        //shoot
        if(Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            c = StartCoroutine(Shooting());
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            c = null;
        }

        //mine
        if (Input.GetMouseButtonUp(1))
        {
            CreateMine();
            points.Enqueue(transform.position);
            if(click != null)
            {
                click();
            }
        }
    }

    private void FixedUpdate()
    {
        //move
        if (h != 0)
        {
            transform.position += Vector3.right * h * speed * Time.fixedDeltaTime;
        }
        if (v != 0)
        {
            transform.position += Vector3.up * v * speed * Time.fixedDeltaTime;
        }

        //rotate
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    Coroutine c = null;
    private float count = 0f;
    IEnumerator Shooting()
    {
        while(isShooting)
        {
            count -= Time.deltaTime;
            if(count < 0f)
            {
                ShootBullet();
                count = fireSpeed;
            }

            yield return null;
        }
    }

    private bool isShooting = false;
    private void ShootBullet()
    {
        Instantiate(bulletPrefab, transform.position + transform.right * 3, transform.rotation);
    }
    private void CreateMine()
    {
        Instantiate(minePrefab, transform.position, Quaternion.identity);
    }

    //array of points that the IK will go too
    public Color pointColour = Color.red;
    public float pointRadius = 1f;
    public Queue<Vector3> points = new Queue<Vector3>();
    public event Action click;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        if(points.Count > 0)
        {
            Gizmos.color = pointColour;
            foreach(var p in points)
            {
                Gizmos.DrawSphere(p, pointRadius);
            }
        }
    }
}