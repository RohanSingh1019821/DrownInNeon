using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuySpawner : MonoBehaviour
{
    private float time = 0.0f;
    public float crabSpawnInterval = 1f;
    public float flyerSpawnInterval = 1f;

    public float ScreenDist = 10;
    public float distVariation = 5;

    public Camera cam;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= crabSpawnInterval)
        {
            SpawnCrab();
            time = 0.0f;
        }
    }

    public void SpawnCrab()
    {
        float x = 0;
        float y = 0;

        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;


        int edge = Random.Range(1, 5);
        
        if (edge == 1)
        {
            x = transform.parent.position.x + camWidth + Random.Range(0, distVariation);
            y = transform.parent.position.y + Random.Range(-camHeight, camHeight);
        }
        else if (edge == 2)
        {
            x = transform.parent.position.x - camWidth - Random.Range(0, distVariation); 
            y = transform.parent.position.y + Random.Range(-camHeight, camHeight);
        }
        else if (edge == 3)
        {
            y = transform.parent.position.y + camHeight + Random.Range(0, distVariation);
            x = transform.parent.position.y + Random.Range(-camWidth, camWidth);
        }
        else
        {
            y = transform.parent.position.y - camHeight - Random.Range(0, distVariation);
            x = transform.parent.position.y + Random.Range(-camWidth, camWidth);
        }

        Vector3 pos = new Vector3(x, y, 0);

        GameObject crabObject = ObjectPool.TInstance.GetCrab(pos);
    }

    public void SpawnFlyer()
    {
        float x = 0;
        float y = 0;

        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;


        int edge = Random.Range(1, 5);

        if (edge == 1)
        {
            x = transform.parent.position.x + camWidth + Random.Range(0, distVariation);
            y = transform.parent.position.y + Random.Range(-camHeight, camHeight);
        }
        else if (edge == 2)
        {
            x = transform.parent.position.x - camWidth - Random.Range(0, distVariation);
            y = transform.parent.position.y + Random.Range(-camHeight, camHeight);
        }
        else if (edge == 3)
        {
            y = transform.parent.position.y + camHeight + Random.Range(0, distVariation);
            x = transform.parent.position.y + Random.Range(-camWidth, camWidth);
        }
        else
        {
            y = transform.parent.position.y - camHeight - Random.Range(0, distVariation);
            x = transform.parent.position.y + Random.Range(-camWidth, camWidth);
        }

        Vector3 pos = new Vector3(x, y, 0);

        GameObject flyerObject = ObjectPool.TInstance.GetFlyer(pos);
    }
}
