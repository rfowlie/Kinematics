using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bombs the player can place
public class Mine : MonoBehaviour
{
    public float detonateTime = 4f;
    public float detonateRadius = 1f;
    private float count = 0f;

    //as we will be reusing these it will be important to be able to change this stuff
    public void Setup(float detonateTime, float detonateRadius)
    {
        this.detonateTime = detonateTime;
        this.detonateRadius = detonateRadius;
    }

    private void Update()
    {
        count += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(count > detonateTime)
        {
            //NEEDS WORK
            //find hits within radius
            Collider[] hits = Physics.OverlapSphere(transform.position, detonateRadius);
            for(int i = 0; i < hits.Length; i++)
            {
                if(hits[i].GetComponent<Enemy>())
                {
                    hits[i].GetComponent<Enemy>().Deactivate();
                }
            }

            gameObject.SetActive(false);
        }
    }
}
