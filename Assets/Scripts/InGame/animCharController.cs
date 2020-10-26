using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class animCharController : MonoBehaviour
{
	float dirX, dirY, rotateAngle;

	public HPDisplay healthCounter;
	public GameObject cam;
	public GameObject hurtOverlay;

	public Transform markerScaler;

	public Joystick joystick;

	public GameObject playerBloww;
	public GameObject playerSucc;

	private bool dead = false;
	private bool fadeToWhite = false;
	public float deathGraceTime = 5;

	public GameObject button;
	public GameObject endButtons;

	private GameManager gameManager = null;

	[SerializeField] float moveSpeed = 2f;
	[SerializeField] float spriteRotoationSpeed = 15f;
	[SerializeField] float immuneTime = 1.5f;

	public float regenTime = 3f;

	private bool immune = false;
	private float immuneFlashTime = 0.08f;
	private bool flash = false;
	public int hitPointsMax = 3;
	public int hitPoints = 3;

	[SerializeField] float bulletSpeed = 100f;

	Animator anim;

	[SerializeField] GameObject explosion;

	Vector3 move;

	[SerializeField] Transform gun;
	[SerializeField] GameObject bulletPrefab;

	private Rigidbody2D rb;

	void Awake()
	{
		GetComponent<CircleCollider2D>().enabled = true;
		GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
		hitPoints = hitPointsMax;
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
		move = Vector2.zero;
		rb = GetComponent<Rigidbody2D>();
		rotateAngle = 0f;
		anim = GetComponent<Animator>();
		anim.speed = 1;
	}

	void FixedUpdate()
	{
		if (dead)
		{
			return;
		}

		Move();
	}

	void Update()
	{
		if (fadeToWhite)
		{
			hurtOverlay.GetComponent<SpriteRenderer>().color += new Color(255, 255, 255, 1f) * Time.deltaTime;
		}

		if (dead)
		{
			return;
		}

		Rotate();
	}

	private void Move()
	{
		dirX = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
		dirY = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));

		if (Mathf.Abs(dirX) <= 0.2f && Mathf.Abs(dirY) <= 0.2f)
        {
            dirX = joystick.Horizontal;
            dirY = joystick.Vertical;
        }

		move = new Vector3(dirX, dirY, 0);

        if (move.magnitude > 0)
		{
			move = move.normalized * moveSpeed;
			anim.SetBool("Moving", true);
		}
		else
		{
			move = new Vector3(0f, 0f, 0f);
			anim.SetBool("Moving", false);
		}

		rb.AddForce(move * Time.fixedDeltaTime);
        if (rb.velocity.magnitude <= 1.2f)
        {
			markerScaler.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
        else
        {
			markerScaler.localScale = new Vector3(1f, 1f, 1f);

		}
	}

	public IEnumerator Flash(float duration, float flashFrequency, Color color, SpriteRenderer spriteRenderer, bool visibleAfter)
	{
		float elapsed = 0f;
		float flashTime = flashFrequency;
		bool flash = false;

		while (elapsed < duration)
		{
			if (flashTime <= 0)
			{
				if (!flash)
				{
					spriteRenderer.color = new Color(255, 255, 255, 0);
					flash = true;
				}
				else
				{
					spriteRenderer.color = color;
					flash = false;
				}
				flashTime = flashFrequency;
			}
			flashTime -= Time.deltaTime;

			elapsed += Time.deltaTime;
			yield return 0;
		}

		if (visibleAfter)
		{
			spriteRenderer.color = new Color(255, 255, 255, 255);
		}
		else
		{
			spriteRenderer.color = new Color(255, 255, 255, 0);
		}
	}

	public IEnumerator CamShake(float duration, float magnitude)
	{
		float elapsed = 0f;

		while (elapsed < duration)
		{
			cam.transform.localPosition = new Vector3(0, 0, 0);
			float x = Random.Range(-1f, 1f) * magnitude;
			float y = Random.Range(-1f, 1f) * magnitude;

			cam.transform.localPosition = new Vector3(x, y, 0);
			elapsed += Time.deltaTime;
			yield return 0;
		}
		cam.transform.localPosition = new Vector3(0, 0, 0);
	}

	public void Fire()
	{
		if (dead)
		{
			return;
		}

		StartCoroutine(Flash(0.08f, 0.004f, new Color(0.07f, 0.88f, 1f, 0.03f), hurtOverlay.GetComponent<SpriteRenderer>(), false));
		StartCoroutine(CamShake(0.1f, 0.08f));
		explosion.SetActive(false);
		explosion.SetActive(true);
		GameObject bulletObject = ObjectPool.TInstance.GetBullet(gun);

	}

	void Rotate()
	{
		if (move.magnitude > 0.1f)
		{
			Vector3 vectorToTarget = (move + transform.position) - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * spriteRotoationSpeed);
		}
	}
	//dracula darker
	//gruvbox
	public void Hit(Vector3 damagePos)
	{

		if (!immune)
		{
			GetComponent<AudioSource>().Play();
			hitPoints -= 1;
			healthCounter.Hit(damagePos);
			immune = true;
			StartCoroutine("ImmuneCool");
			StopCoroutine("Regen");
			StartCoroutine("Regen");
			StartCoroutine(Flash(0.2f, 0.01f, new Color(0.925f, 0.113f, 0.137f, 0.8f), hurtOverlay.GetComponent<SpriteRenderer>(), false));
			StartCoroutine(Flash(immuneTime, 0.1f, new Color(255, 255, 255, 255), GetComponent<SpriteRenderer>(), true));
			StartCoroutine(CamShake(0.5f, 0.3f));
			if (hitPoints <= 0)
			{
				StartCoroutine(Flash(deathGraceTime, 0.1f, new Color(255, 255, 255, 255), GetComponent<SpriteRenderer>(), false));
				StartCoroutine("Dying");
				print("Player died");
			}
		}
	}

	private IEnumerator Regen()
	{
		yield return new WaitForSeconds(regenTime);
		hitPoints += 1;
		healthCounter.Regen();
		if (hitPoints < hitPointsMax)
        {
			StartCoroutine("Regen");
		}
	}

	private IEnumerator ImmuneCool()
	{
		yield return new WaitForSeconds(immuneTime);
		immune = false;
	}

	public void KillPlayer()
    {
		StartCoroutine(Flash(deathGraceTime, 0.1f, new Color(255, 255, 255, 255), GetComponent<SpriteRenderer>(), false));
		StartCoroutine("Dying");
		print("Player died");
	}

	private IEnumerator Dying()
    {
		StopCoroutine("Regen");
		playerSucc.SetActive(true);
		yield return new WaitForSeconds(deathGraceTime);
		button.SetActive(false);
		endButtons.SetActive(true);
		fadeToWhite = true;
		playerSucc.SetActive(false);
		playerBloww.SetActive(true);
		StopCoroutine("Flash");
		GetComponent<CircleCollider2D>().enabled = false;
		GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
		gameManager.PlayerKilled();
		dead = true;
		yield return new WaitForSeconds(3f);
		playerBloww.SetActive(false);
	}

}
