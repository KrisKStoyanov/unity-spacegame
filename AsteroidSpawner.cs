using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    public static AsteroidSpawner current;

    public GameObject gameView;

    int spawnLocation;

    public float spawnInterval;
    public float spawnWait;

    private void Awake()
    {
        current = this;
    }

    // Use this for initialization
    void Start () {
        spawnWait = spawnInterval;
        StartCoroutine(SpawnAsteroids());
	}
	
    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            spawnInterval -= 1 * Time.deltaTime;
            if(spawnInterval < 0)
            {
                SpawnAsteroid();
                spawnInterval += Random.Range(spawnWait/2,spawnWait*2);
            }
       
            yield return null;
        }
    }
    void SpawnAsteroid()
    {
        GameObject obj = ObjectPooler.current.GetPooledObject();

        if (obj == null) return;
        spawnLocation = Random.Range(1, 5);
        AsteroidProperties prop = obj.GetComponent<AsteroidProperties>();
        prop.health = Mathf.RoundToInt(Random.Range(1, 4));
        if(prop.health == 1)
        {
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        if(prop.health == 2)
        {
            obj.transform.localScale = new Vector3(0.75f,0.75f,0.75f);
        }
        if(prop.health == 3)
        {
            obj.transform.localScale = new Vector3(0.9f,0.9f,0.9f);
        }
        /*if(spawnLocation == 1)
        {
            obj.transform.position += new Vector3(gameView.GetComponent<BoxCollider2D>().bounds.extents.x, gameView.GetComponent<BoxCollider2D>().bounds.extents.y);
        }
        else if(spawnLocation == 2)
        {
            obj.transform.position += new Vector3(gameView.GetComponent<BoxCollider2D>().bounds.extents.x, -gameView.GetComponent<BoxCollider2D>().bounds.extents.y);
        }
        else if(spawnLocation == 3)
        {
            obj.transform.position += new Vector3(-gameView.GetComponent<BoxCollider2D>().bounds.extents.x,gameView.GetComponent<BoxCollider2D>().bounds.extents.y);
        }
        else if(spawnLocation == 4)
        {
            obj.transform.position += new Vector3(-gameView.GetComponent<BoxCollider2D>().bounds.extents.x, -gameView.GetComponent<BoxCollider2D>().bounds.extents.y);
        }*/
        if(spawnLocation == 1)
        {
            obj.transform.position = new Vector3(gameView.GetComponent<BoxCollider2D>().bounds.extents.x, Random.Range(gameView.GetComponent<BoxCollider2D>().bounds.extents.y,-gameView.GetComponent<BoxCollider2D>().bounds.extents.y));
        }
        else if(spawnLocation == 2)
        {
            obj.transform.position = new Vector3(-gameView.GetComponent<BoxCollider2D>().bounds.extents.x, Random.Range(gameView.GetComponent<BoxCollider2D>().bounds.extents.y, -gameView.GetComponent<BoxCollider2D>().bounds.extents.y));
        }
        else if(spawnLocation == 3)
        {
            obj.transform.position = new Vector3(-gameView.GetComponent<BoxCollider2D>().bounds.extents.y, Random.Range(gameView.GetComponent<BoxCollider2D>().bounds.extents.x, gameView.GetComponent<BoxCollider2D>().bounds.extents.y));
        }
        else if(spawnLocation == 4)
        {
            obj.transform.position = new Vector3(gameView.GetComponent<BoxCollider2D>().bounds.extents.y, Random.Range(-gameView.GetComponent<BoxCollider2D>().bounds.extents.x, -gameView.GetComponent<BoxCollider2D>().bounds.extents.y));
        }
        obj.transform.rotation = transform.rotation;
        //obj.AddComponent<AsteroidMovement>();
        obj.SetActive(true);
    }
}
