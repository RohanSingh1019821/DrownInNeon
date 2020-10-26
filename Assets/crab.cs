using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crab : MonoBehaviour
{
    private GameObject aimPoint = null;
    public Transform firePos = null;
    public GameObject explosion;

    private bool dead;

    private Rigidbody2D rb = null;
    public float speed = 10.0f;

    private int hitPoints = 2;

    public float spitRange = 50.0f;
    public Vector3 dist;

    public bool firing = false;



    private float coolTimer = 0.0f;
    public float coolTime = 3.0f;
    
    private GameManager gameManager = null;
    private Animator anim = null;

    void OnEnable()
    {

        dead = false;
        anim = GetComponent<Animator>();
        anim.SetBool("Spit", false);
        anim.SetBool("Move", true);
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        explosion.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        hitPoints = 2;
        firing = false;
        aimPoint = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //transform.LookAt(aimPoint.transform, transform.right);
        //transform.up = aimPoint.transform.position - transform.position;
        Vector3 dir = aimPoint.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg -90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        dist = aimPoint.transform.position - this.transform.position;
        if (dist.magnitude <= spitRange && coolTimer <= 0.0f && !firing && !dead)
        {
            coolTimer = coolTime;
            StartCoroutine("Fire");
            anim.SetBool("Spit", true);
            anim.SetBool("Move", false);
        }
        else if (!firing && !dead) 
        {
            if (dist.magnitude > spitRange)
            {
                Move();
            }

            anim.SetBool("Spit", false);
            anim.SetBool("Move", true);
            coolTimer -= Time.deltaTime;
        }
    }

    void Move()
    {
        rb.velocity = transform.up * speed;
    }


    public IEnumerator Fire()
    {
        if (!firing)
        {
            firing = true;
            /* Wait for the specified delay */
            yield return new WaitForSeconds(1.5f);
            fire();
            firing = false;
        }

        yield break;
    }

    void fire()
    {
        firePos.rotation = this.transform.rotation;
        GameObject spitObject = ObjectPool.TInstance.GetSpit(firePos);
    }

    public void Hit()
    {
        hitPoints -= 1;

        if (hitPoints <= 0)
        {
            GetComponent<AudioSource>().Play();
            dead = true;
            StopCoroutine("Fire");
            gameManager.EnemyKilled();
            StartCoroutine("Die");
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            explosion.SetActive(true);
        }
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        //yield break;
    }
}
