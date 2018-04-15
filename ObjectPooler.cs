using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler current;

    public GameObject[] pooledObject;
    public int pooledAmount;
    public bool willGrow = true;

    List<GameObject> pooledObjects;

    public int pooledAmountTwo;
    public int pooledAmountThree;
    public int pooledAmountFour;
    public int pooledAmountFive;
    public int pooledAmountSix;
    public int pooledAmountSeven;
    public int pooledAmountEight;
    public int pooledAmountNine;
    public int pooledAmountTen;
    public int pooledAmountEleven;
    public int pooledAmountTwelve;
    public int pooledAmountThirteen;

    List<GameObject> pooledObjectsTwo;
    List<GameObject> pooledObjectsThree;
    List<GameObject> pooledObjectsFour;
    List<GameObject> pooledObjectsFive;
    List<GameObject> pooledObjectsSix;
    List<GameObject> pooledObjectsSeven;
    List<GameObject> pooledObjectsEight;
    List<GameObject> pooledObjectsNine;
    List<GameObject> pooledObjectsTen;
    List<GameObject> pooledObjectsEleven;
    List<GameObject> pooledObjectsTwelve;
    List<GameObject> pooledObjectsThirteen;

    private void Awake()
    {
        current = this;
    }
    // Use this for initialization
    void Start () {
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[0]);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        pooledObjectsTwo = new List<GameObject>();
        for(int i = 0; i < pooledAmountTwo; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[1]);
            obj.SetActive(false);
            pooledObjectsTwo.Add(obj);
        }
        pooledObjectsThree = new List<GameObject>();
        for (int i = 0; i < pooledAmountThree; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[2]);
            obj.SetActive(false);
            pooledObjectsThree.Add(obj);
        }
        pooledObjectsFour = new List<GameObject>();
        for (int i = 0; i < pooledAmountFour; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[3]);
            obj.SetActive(false);
            pooledObjectsFour.Add(obj);
        }
        pooledObjectsFive = new List<GameObject>();
        for (int i = 0; i < pooledAmountFive; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[4]);
            obj.SetActive(false);
            pooledObjectsFive.Add(obj);
        }
        pooledObjectsSix = new List<GameObject>();
        for (int i = 0; i < pooledAmountSix; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[5]);
            obj.SetActive(false);
            pooledObjectsSix.Add(obj);
        }
        pooledObjectsSeven = new List<GameObject>();
        for (int i = 0; i < pooledAmountSeven; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[6]);
            obj.SetActive(false);
            pooledObjectsSeven.Add(obj);
        }
        pooledObjectsEight = new List<GameObject>();
        for (int i = 0; i < pooledAmountEight; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[7]);
            obj.SetActive(false);
            pooledObjectsEight.Add(obj);
        }
        pooledObjectsNine = new List<GameObject>();
        for (int i = 0; i < pooledAmountNine; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[8]);
            obj.SetActive(false);
            pooledObjectsNine.Add(obj);
        }
        pooledObjectsTen = new List<GameObject>();
        for (int i = 0; i < pooledAmountTen; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[9]);
            obj.SetActive(false);
            pooledObjectsTen.Add(obj);
        }
        pooledObjectsEleven = new List<GameObject>();
        for (int i = 0; i < pooledAmountEleven; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[10]);
            obj.SetActive(false);
            pooledObjectsEleven.Add(obj);
        }
        pooledObjectsTwelve = new List<GameObject>();
        for (int i = 0; i < pooledAmountTwelve; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[11]);
            obj.SetActive(false);
            pooledObjectsTwelve.Add(obj);
        }
        pooledObjectsThirteen = new List<GameObject>();
        for (int i = 0; i < pooledAmountThirteen; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[12]);
            obj.SetActive(false);
            pooledObjectsThirteen.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[0]);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }
    public GameObject GetPooledObjectTwo()
    {
        for (int i = 0; i < pooledObjectsTwo.Count; i++)
        {
            if (!pooledObjectsTwo[i].activeInHierarchy)
            {
                return pooledObjectsTwo[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[1]);
            pooledObjectsTwo.Add(obj);
            return obj;
        }
        return null;
    }
    public GameObject GetPooledObjectThree()
    {
        for (int i = 0; i < pooledObjectsThree.Count; i++)
        {
            if (!pooledObjectsThree[i].activeInHierarchy)
            {
                return pooledObjectsThree[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[2]);
            pooledObjectsThree.Add(obj);
            return obj;
        }
        return null;
    }
    public GameObject GetPooledObjectFour()
    {
        for (int i = 0; i < pooledObjectsFour.Count; i++)
        {
            if (!pooledObjectsFour[i].activeInHierarchy)
            {
                return pooledObjectsFour[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[3]);
            pooledObjectsFour.Add(obj);
            return obj;
        }
        return null;
    }
    public GameObject GetPooledObjectFive()
    {
        for (int i = 0; i < pooledObjectsFive.Count; i++)
        {
            if (!pooledObjectsFive[i].activeInHierarchy)
            {
                return pooledObjectsFive[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[4]);
            pooledObjectsFive.Add(obj);
            return obj;
        }
        return null;
    }
    public GameObject GetPooledObjectSix()
    {
        for (int i = 0; i < pooledObjectsSix.Count; i++)
        {
            if (!pooledObjectsSix[i].activeInHierarchy)
            {
                return pooledObjectsSix[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[5]);
            pooledObjectsSix.Add(obj);
            return obj;
        }
        return null;
    }
    public GameObject GetPooledObjectSeven()
    {
        for (int i = 0; i < pooledObjectsSeven.Count; i++)
        {
            if (!pooledObjectsSeven[i].activeInHierarchy)
            {
                return pooledObjectsSeven[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[6]);
            pooledObjectsSeven.Add(obj);
            return obj;
        }
        return null;
    }
    public GameObject GetPooledObjectEight()
    {
        for (int i = 0; i < pooledObjectsSeven.Count; i++)
        {
            if (!pooledObjectsEight[i].activeInHierarchy)
            {
                return pooledObjectsEight[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[7]);
            pooledObjectsEight.Add(obj);
            return obj;
        }
        return null;
    }
    public GameObject GetPooledObjectNine()
    {
        for (int i = 0; i < pooledObjectsNine.Count; i++)
        {
            if (!pooledObjectsNine[i].activeInHierarchy)
            {
                return pooledObjectsNine[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[8]);
            pooledObjectsNine.Add(obj);
            return obj;
        }
        return null;
    }
    public GameObject GetPooledObjectTen()
    {
        for (int i = 0; i < pooledObjectsTen.Count; i++)
        {
            if (!pooledObjectsTen[i].activeInHierarchy)
            {
                return pooledObjectsTen[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[9]);
            pooledObjectsTen.Add(obj);
            return obj;
        }
        return null;
    }
    public GameObject GetPooledObjectEleven()
    {
        for (int i = 0; i < pooledObjectsEleven.Count; i++)
        {
            if (!pooledObjectsEleven[i].activeInHierarchy)
            {
                return pooledObjectsEleven[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[10]);
            pooledObjectsEleven.Add(obj);
            return obj;
        }
        return null;
    }
    public GameObject GetPooledObjectTwelve()
    {
        for (int i = 0; i < pooledObjectsTwelve.Count; i++)
        {
            if (!pooledObjectsTwelve[i].activeInHierarchy)
            {
                return pooledObjectsTwelve[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[11]);
            pooledObjectsTwelve.Add(obj);
            return obj;
        }
        return null;
    }
    public GameObject GetPooledObjectThirteen()
    {
        for (int i = 0; i < pooledObjectsThirteen.Count; i++)
        {
            if (!pooledObjectsThirteen[i].activeInHierarchy)
            {
                return pooledObjectsThirteen[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject[12]);
            pooledObjectsThirteen.Add(obj);
            return obj;
        }
        return null;
    }
}
