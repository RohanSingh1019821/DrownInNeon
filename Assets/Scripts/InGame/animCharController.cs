using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animCharController : MonoBehaviour
{
    float dirX, dirY, rotateAngle;

    [SerializeField] float moveSpeed = 2f;

    [SerializeField] float bulletSpeed = 100f;

    Animator anim;

    [SerializeField] Transform gun;
    [SerializeField] GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rotateAngle = 0f;
        anim = GetComponent<Animator>();
        anim.speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Fire();
        Rotate();
    }

    private void Move()
    {
        dirX = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        dirY = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));

        transform.position = new Vector2(dirX * moveSpeed * Time.deltaTime + transform.position.x,
            dirY * moveSpeed * Time.deltaTime + transform.position.y);
    }

    public void Fire()
    {
		//      if (Input.GetButtonDown("Fire1"))
		//      {
		//          //var firedBullet = Instantiate(bullet, gun.position, gun.rotation);
		//          //firedBullet.AddForce(gun.up * bulletSpeed);

		//	GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
		//	Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		//	rb.AddForce(gun.up * bulletSpeed, ForceMode2D.Impulse);
		//	Destroy(bullet, 2f);
		//}

		GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.AddForce(gun.up * bulletSpeed, ForceMode2D.Impulse);
		Destroy(bullet, 2f);
	}

	void Rotate()
	{
		if (dirX == 0 && dirY == 1)
		{
			rotateAngle = 0;
			anim.speed = 1;
			anim.SetInteger("Direction", 1);
		}

		if (dirX == 1 && dirY == 1)
		{
			rotateAngle = -45f;
			anim.speed = 1;
			anim.SetInteger("Direction", 2);
		}

		if (dirX == 1 && dirY == 0)
		{
			rotateAngle = -90f;
			anim.speed = 1;
			anim.SetInteger("Direction", 3);
		}

		if (dirX == 1 && dirY == -1)
		{
			rotateAngle = -135f;
			anim.speed = 1;
			anim.SetInteger("Direction", 4);
		}

		if (dirX == 0 && dirY == -1)
		{
			rotateAngle = -180f;
			anim.speed = 1;
			anim.SetInteger("Direction", 5);
		}

		if (dirX == -1 && dirY == -1)
		{
			rotateAngle = -225f;
			anim.speed = 1;
			anim.SetInteger("Direction", 6);
		}

		if (dirX == -1 && dirY == 0)
		{
			rotateAngle = -270f;
			anim.speed = 1;
			anim.SetInteger("Direction", 7);
		}

		if (dirX == -1 && dirY == 1)
		{
			rotateAngle = -315f;
			anim.speed = 1;
			anim.SetInteger("Direction", 8);
		}

		if (dirX == 0 && dirY == 0)
		{
			anim.speed = 0;
		}

		gun.rotation = Quaternion.Euler(0f, 0f, rotateAngle);

	}

}
