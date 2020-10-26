using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject sprite;
    public GameObject trail;
    private BoxCollider2D bc2D;


    public float bulletSpeed = 20f;
    
    private float life;
    public float maxLife = 1f;

    private bool exploded = false;

    private void OnEnable()
    {
        bc2D = GetComponent<BoxCollider2D>();
        //bc2D.enabled = true;
        hitEffect.SetActive(false);
        sprite.SetActive(true);
        trail.SetActive(true);
        exploded = false;
        Rigidbody2D grb = GetComponent<Rigidbody2D>();
        grb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        life = maxLife;
        StartCoroutine("Enabled");
    }

    private void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0f && !exploded)
        {
            bc2D.enabled = false;
            StartCoroutine("Explode");
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer != 8 && !exploded && col.gameObject.layer != 11)
        {

            bc2D.enabled = false;
            StartCoroutine("Explode");
            if (col.gameObject.layer == 9)
            {
                col.gameObject.GetComponent<crab>().Hit();
            }
            else if (col.gameObject.layer == 13)
            {
                col.gameObject.GetComponent<Flyer>().Hit();
            }
        }
        
    }

    public IEnumerator Explode()
    {
        if (!exploded)
        {
            explode();
        }
        exploded = true;
        /* Wait for the specified delay */
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void explode()
    {
        hitEffect.SetActive(true);
        trail.SetActive(false);
        sprite.SetActive(false);
    }

    private IEnumerator Enabled()
    {
        bc2D.enabled = false;
        yield return new WaitForSeconds(0.05f);
        bc2D.enabled = true;
    }
}
