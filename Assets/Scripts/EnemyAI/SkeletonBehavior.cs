using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehavior : MonoBehaviour {

    [SerializeField]
    NonPlayerCharacter npcCore;

    private bool IsHurt;

    private Animator animator;

    private SpriteRenderer sprite;

    private BoxCollider2D boxCollider;

    private Rigidbody2D rigidBody;

    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private float InvincibilityFrames;

    private float hurtAnimTimer;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        npcCore = GetComponent<NonPlayerCharacter>();
        animator = GetComponent<Animator>();
        Debug.Log("skeleton");
	}

    // Update is called once per frame
    void Update() {
        SyncUpAnimations();
        StopHurtAnimation();
    }

    private void StopHurtAnimation()
    {
        if (IsHurt == true)
        {
            hurtAnimTimer += Time.deltaTime;
            if(hurtAnimTimer >= InvincibilityFrames)
            {
                IsHurt = false;
                hurtAnimTimer = 0;
            }
        }
    }
    private void OnColliderEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (IsHurt == false)
            {
                ProjectileHandler(collision);
                GetHurt();
            }
        }
    }


    private void OnColliderExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (IsHurt == true)
            {
                IsHurt = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (IsHurt == false)
            {
                ProjectileHandler(collision);
                GetHurt();
            }
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (IsHurt == true)
            {
                IsHurt = false;
            }
        }
    }

    private void ProjectileHandler(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if(bullet.bulletDisappearsUponCollision == true)
        {
            Destroy(collision.gameObject);
        }
    }

    private void SyncUpAnimations()
    {
        animator.SetBool("hurt", IsHurt);
    }

    private void GetHurt()
    {
        IsHurt = true;
        npcCore.HealthPoints--;
        if(npcCore.HealthPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Transform smokeOrigin = gameObject.transform;
        Instantiate(deathEffect, smokeOrigin.position, smokeOrigin.rotation);
        sprite.enabled = false;
        rigidBody.gravityScale = 0;
        boxCollider.isTrigger = true;
        Destroy(gameObject, 0.6f);
    }

    private void FixedUpdate()
    {

    }
}
