using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuySpawner : MonoBehaviour
{
    private float time = 0.0f;

    private float timer = 0.0f;
    private float crabSpawnInterval = 8f;
    private float flyerSpawnInterval = 8f;

    public float ScreenDist = 9;
    public float distVariation = 5;

    public Camera cam;

    public Vector2 bounds = new Vector2(50, 50); 

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 24.0f)
        {
            crabSpawnInterval -= 0.4f;
            flyerSpawnInterval -= 0.4f;
        }


        if (flyerSpawnInterval <= 0.2f)
        {
            flyerSpawnInterval = 0.2f;
        }

        if (crabSpawnInterval <= 0.2f)
        {
            crabSpawnInterval = 0.2f;
        }

        time += Time.deltaTime;
        if (time >= crabSpawnInterval)
        {
            SpawnCrab();
            time = 0.0f;
        }

        time += Time.deltaTime;
        if (time >= flyerSpawnInterval)
        {
            SpawnFlyer();
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
        if (Mathf.Abs(pos.x) < bounds.x)
        {
            if (Mathf.Abs(pos.y) < bounds.y)
            {
                GameObject crabObject = ObjectPool.TInstance.GetCrab(pos);
            }
        }

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
        if (Mathf.Abs(pos.x) < bounds.x)
        {
            if (Mathf.Abs(pos.y) < bounds.y)
            {
                GameObject flyerObject = ObjectPool.TInstance.GetFlyer(pos);
            }
        }
    }
}
