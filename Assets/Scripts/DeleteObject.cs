using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObject : MonoBehaviour
{
    public float objectDuration = 5f;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
       // Invoke("DestroyObject", objectDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.position, transform.position) < 10)
        {
            Destroy(gameObject);
        }
    }

    void DestroyObject()
    {
        //Destroy(gameObject);
    }
}
