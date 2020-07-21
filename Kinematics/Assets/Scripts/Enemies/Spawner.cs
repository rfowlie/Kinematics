using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//use the center of the map and spawn at certain magnitude away, random x and y 
public class Spawner : MonoBehaviour
{
    public float min = 5f;
    public float max = 8f;
    public GameObject enemyPrefab = null;
    //active list
    private List<GameObject> eA = new List<GameObject>();
    //inactive queue
    private Queue<GameObject> eI = new Queue<GameObject>();
    public bool isSpawn = false;


    private void OnEnable()
    {
        Enemy.Remove += RemoveFromList;
    }

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

    private void RemoveFromList(GameObject obj)
    {
        eA.Remove(obj);
        eI.Enqueue(obj);

        obj.SetActive(false);
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
        if(eI.Count > 0)
        {
            GameObject e = eI.Dequeue();
            e.transform.position = CreateRandomPoint();
            e.transform.rotation = Quaternion.LookRotation(transform.position - e.transform.position);
            e.gameObject.SetActive(true);
            eA.Add(e);
            return;
        }
        //no available inactive enemy objects, create a new one
        else
        {
            Vector3 temp = CreateRandomPoint();
            eA.Add(Instantiate(enemyPrefab,
                                       temp,
                                       Quaternion.LookRotation(transform.position - temp)));
        }        
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
