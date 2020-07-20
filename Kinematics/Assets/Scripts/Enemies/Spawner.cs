using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//use the center of the map and spawn at certain magnitude away, random x and y 
public class Spawner : MonoBehaviour
{
    //public Vector3 spawnCenter = Vector3.zero;
    public float min = 5f;
    public float max = 8f;
    public GameObject enemyPrefab = null;
    private List<GameObject> allEnemies = new List<GameObject>();
    public bool isSpawn = false;

    private void Start()
    {
        //check if min and max are correct
        if(min > max)
        {
            float temp = min;
            min = max;
            max = temp;
        }
    }

    //get random vector coordinates within specified range
    private Vector3 CreateRandomPoint()
    {
        //get random rotation
        int angle = Random.Range(0, 360);
        //rotate this
        transform.eulerAngles = new Vector3(angle, 90, 0);
        //get length
        float distance = Random.Range(min, max);
        //spawn 
        return transform.forward * distance;
    }

    private void SpawnEnemy()
    {
        foreach (var e in allEnemies)
        {
            if (e.gameObject.activeSelf == false)
            {
                //set position and set active
                e.transform.position = CreateRandomPoint();
                e.transform.rotation = Quaternion.LookRotation(transform.position - e.transform.position);
                e.gameObject.SetActive(true);
                return;
            }
        }

        //create new enemy as none available
        Vector3 temp = CreateRandomPoint();
        allEnemies.Add(Instantiate(enemyPrefab,
                                   temp,
                                   Quaternion.LookRotation(transform.position - temp)));
    }

    public float spawnTime = 1.0f;
    private float count = 0f;
    private void Update()
    {
        if(isSpawn)
        {
            if (count < spawnTime)
            {
                count += Time.deltaTime;
            }
            else
            {
                count = 0f;
                SpawnEnemy();
            }
        }
    }
}
