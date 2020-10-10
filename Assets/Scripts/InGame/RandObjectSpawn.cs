using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class RandObjectSpawn : MonoBehaviour
{
    public GameObject[] gameObjects;
    public Transform player;
    public Transform playerTransform;
    private int spawnRangeX = 20;
    private int spawnRangeY = 20;

    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnObject", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = player;
    }

    void SpawnObject()
    {
        int objectIndex = Random.Range(0, gameObjects.Length);
        Vector3 spawnPos = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY));

        obj = Instantiate(gameObjects[objectIndex], playerTransform.position + spawnPos, gameObjects[objectIndex].transform.rotation);

        Destroy(obj, 8f);

    }

}
