using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    public static ObjectPool TInstance { get { return instance; } }

    public GameObject crabPrefab;
    public int crabAmount = 20;
    private List<GameObject> crabs;

    public GameObject flyerPrefab;
    public int flyerAmount = 20;
    private List<GameObject> flyers;

    public GameObject bulletPrefab;
    public int bulletAmount = 20;
    private List<GameObject> bullets;

    public GameObject spitPrefab;
    public int spitAmount = 20;
    private List<GameObject> spits;

    void Awake()
    {
        instance = this;
        crabs = new List<GameObject>(crabAmount);
        for (int i = 0; i < crabAmount; i++)
        {
            GameObject prefabInstance = Instantiate(crabPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            crabs.Add(prefabInstance);
        }

        flyers = new List<GameObject>(flyerAmount);
        for (int i = 0; i < flyerAmount; i++)
        {
            GameObject prefabInstance = Instantiate(flyerPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            flyers.Add(prefabInstance);
        }

        bullets = new List<GameObject>(bulletAmount);
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject prefabInstance = Instantiate(bulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            bullets.Add(prefabInstance);
        }

        spits = new List<GameObject>(spitAmount);
        for (int i = 0; i < spitAmount; i++)
        {
            GameObject prefabInstance = Instantiate(spitPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            spits.Add(prefabInstance);
        }
    }

    public GameObject GetCrab(Vector3 spawnPos)
    {
        foreach (GameObject crab in crabs)
        {
            if (!crab.activeInHierarchy)
            {
                crab.transform.position = spawnPos;
                crab.SetActive(true);
                return crab;
            }
        }
        return null;
    }

    public GameObject GetFlyer(Vector3 spawnPos)
    {
        foreach (GameObject flyer in flyers)
        {
            if (!flyer.activeInHierarchy)
            {
                flyer.transform.position = spawnPos;
                flyer.SetActive(true);
                return flyer;
            }
        }
        return null;
    }

    public GameObject GetBullet(Transform spawnPos)
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = spawnPos.position;
                bullet.transform.rotation = spawnPos.rotation;
                bullet.SetActive(true);
                return bullet;
            }
        }
        return null;
    }

    public GameObject GetSpit(Transform spawnPos)
    {
        foreach (GameObject spit in spits)
        {
            if (!spit.activeInHierarchy)
            {
                spit.transform.position = spawnPos.position;
                spit.transform.rotation = spawnPos.rotation;
                spit.SetActive(true);
                return spit;
            }
        }
        return null;
    }
}
