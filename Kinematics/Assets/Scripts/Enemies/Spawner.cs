using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//things that will be an object that needs pooling
public class PoolObject : MonoBehaviour
{
    public Pooler pooler;
}

//work in progress....
public class Pooler : MonoBehaviour
{
    //constructor
    public Pooler(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public GameObject prefab;
    public List<GameObject> active = new List<GameObject>();
    public Queue<GameObject> available = new Queue<GameObject>();

    public void RemoveFromList(GameObject obj)
    {
        if(active.Contains(obj))
        {
            active.Remove(obj);
            available.Enqueue(obj);
            obj.SetActive(false);
        }
        else
        {
            Debug.LogError("This object was not in the active list, something went wrong!!");
        }        
    }

    public GameObject GetAvailable()
    {
        if(available.Count > 0)
        {
            return available.Dequeue();
        }
        else
        {
            GameObject temp = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            active.Add(temp);
            return temp; 
        }
    }
}

//use the center of the map and spawn at certain magnitude away, random x and y 
public class Spawner : MonoBehaviour
{
    public float min = 5f;
    public float max = 8f;
    public GameObject prefab = null;
    //active list
    private List<GameObject> active = new List<GameObject>();
    //inactive queue
    private Queue<GameObject> available = new Queue<GameObject>();
    public bool isSpawn = false;


    private void OnEnable()
    {
        Enemy.Recycle += RemoveFromList;
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

    public void RemoveFromList(GameObject obj)
    {
        if (active.Contains(obj))
        {
            active.Remove(obj);
            available.Enqueue(obj);
            obj.SetActive(false);
        }
        else
        {
            Debug.LogError("This object was not in the active list, something went wrong!!");
        }
    }

    public GameObject GetAvailable()
    {
        if (available.Count > 0)
        {
            return available.Dequeue();
        }
        else
        {
            return Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
    
    private void SpawnEnemy()
    {
        GameObject e = GetAvailable();
        e.transform.position = CreateRandomPoint();
        e.transform.rotation = Quaternion.LookRotation(transform.position - e.transform.position);
        e.gameObject.SetActive(true);
        active.Add(e);
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
