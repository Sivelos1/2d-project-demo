using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ObjectClass
{
    Player,
    Enemy,
    NPC,
    Prop
}

public class BulletInteractivity : MonoBehaviour {

    [SerializeField]
    private float maximumHitPoints;

    [SerializeField]
    private float currentHitPoints;

    [SerializeField]
    private bool canBeHurt;

    [SerializeField]
    private float damageMultiplier = 1;

    [SerializeField]
    private bool canDie;

    [SerializeField]
    private float destructionDelayBeforeDeath;

    [SerializeField]
    private Collider2D affectedCollider;

    [SerializeField]
    private float invincibilityFramesInSeconds;

    [SerializeField]
    private float amountOfFlickersPerSecond;

    [SerializeField]
    private bool invincible;

    [SerializeField]
    private List<Emmission> emissionsOnDeath;

    private SpriteRenderer spriteRenderer;

    private float invincibilityTimer, flickerTimer;

    private Rigidbody2D rigidBody;

    private bool Dying;

    // Use this for initialization
    void Start () {
        if(emissionsOnDeath == null)
        {
            emissionsOnDeath = new List<Emmission>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        if(affectedCollider == null)
        {
            affectedCollider = GetComponent<Collider2D>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        ManageInvincibility();
    }

    private void FlickerSprite()
    {
        flickerTimer += Time.deltaTime;
        if(flickerTimer >= amountOfFlickersPerSecond)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            flickerTimer = 0;
        }
        
    }

    private void ManageInvincibility()
    {
        if(invincible == true)
        {
            invincibilityTimer += Time.deltaTime;
            if (invincibilityTimer >= invincibilityFramesInSeconds)
            {
                invincible = false;
                invincibilityTimer = 0;
            }
            FlickerSprite();
        }

        MakeSureObjectDoesntStayInvisible();

    }

    private void MakeSureObjectDoesntStayInvisible()
    {
        if (invincible != true && spriteRenderer.enabled == false)
        {
            spriteRenderer.enabled = true;
        }
    }

    private void ModifyHP(float deltaHP)
    {
        currentHitPoints += (deltaHP * damageMultiplier);
        if(currentHitPoints <= 0 && canDie == true)
        {
            Die();
        }
        if(invincibilityFramesInSeconds > 0 && (deltaHP * damageMultiplier) <= 0 && Dying == false)
        {
            invincible = true;
        }
        else
        {
            Debug.Log("lol no dice");
        }
    }

    private void Die()
    {
        Dying = true;
        Transform emmisionOrigin = gameObject.transform;
        spriteRenderer.enabled = false;
        rigidBody.velocity = Vector2.zero;
        rigidBody.gravityScale = 0;
        affectedCollider.isTrigger = true;
        foreach (Emmission emm in emissionsOnDeath)
        {
            if(emm == null)
            {
                continue;
            }
            if(emm.GetChanceToFire() == true)
            {
                Debug.Log("ouch my bones");
                emm.GetPhysics();
                Rigidbody2D drop;
                drop = Instantiate(emm.EmissionPhysics, emmisionOrigin.position, emmisionOrigin.rotation) as Rigidbody2D;
                drop.AddForce(emm.Trajectory);
            }
            

        }
        Destroy(gameObject, destructionDelayBeforeDeath);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if(canBeHurt == true && invincible == false)
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                if (bullet == null)
                {

                    ModifyHP(-1);
                }
                else
                {
                    ModifyHP(-bullet.GetBaseDamage());
                }
            }
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Bullet"))
    //    {
    //        if (canBeHurt == true || invincible == false)
    //        {
    //            ReduceHP();
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (canBeHurt == true && invincible == false)
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                if (bullet == null)
                {

                    ModifyHP(-1);
                }
                else
                {
                    ModifyHP(-bullet.GetBaseDamage());
                }
            }
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Bullet"))
    //    {
    //        if (canBeHurt == true || invincible == false)
    //        {
    //            ReduceHP();
    //        }
    //    }
    //}

    public bool GetIsHurt()
    {
        return invincible;
    }

    public float GetCurrentHP()
    {
        return currentHitPoints;
    }

    public float GetMaxHP()
    {
        return maximumHitPoints;
    }

}
