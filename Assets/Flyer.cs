using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{
    private GameObject aimPoint = null;
    public Transform firePos = null;
    public GameObject explosion;

    private bool dead;

    private Rigidbody2D rb = null;
    public float speed = 10.0f;

    private int hitPoints = 1;

    public float spitRange = 50.0f;
    public Vector3 dist;

    public SpriteRenderer sprite;
    public SpriteRenderer mouthSprite;

    private float coolTimer = 0.0f;
    public float coolTime = 3.0f;

    private GameManager gameManager = null;
    public  Animator anim = null;

    void OnEnable()
    {
        dead = false;
        GetComponent<Animator>().SetBool("Move", true);
        anim.SetBool("Mouth", false);
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        explosion.SetActive(false);
        mouthSprite.enabled = true;
        sprite.enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        hitPoints = 1;
        aimPoint = GameObject.FindWithTag("InFrontOfPlayer");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //transform.LookAt(aimPoint.transform, transform.right);
        //transform.up = aimPoint.transform.position - transform.position;

        if (dist.magnitude <= spitRange)
        {
            anim.SetBool("Mouth", true);
        }
        else
        {
            anim.SetBool("Mouth", false);
        }

        dist = aimPoint.transform.position - this.transform.position;
        if (coolTimer <= 0.0f && !dead)
        {
            GetComponent<Animator>().SetBool("Move", true);
            Vector3 dir = aimPoint.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            coolTimer = coolTime;
            Move();
        }
        else if (!dead)
        {
            GetComponent<Animator>().SetBool("Move", false);
            coolTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
         if (col.gameObject.layer == 8)
         {
            col.gameObject.GetComponent<animCharController>().Hit(transform.position);
         }
     
    }

    void Move()
    {
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
    }

    public void Hit()
    {
        hitPoints -= 1;

        if (hitPoints <= 0)
        {
            GetComponent<AudioSource>().Play();
            dead = true;
            gameManager.EnemyKilled();
            StartCoroutine("Die");
            mouthSprite.enabled = false;
            sprite.enabled = false;
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
