using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer != 8)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);
            if (col.gameObject.layer == 9)
            {
                col.gameObject.GetComponent<BadGuy>().Killed();
            }
        }
        
    }
}
