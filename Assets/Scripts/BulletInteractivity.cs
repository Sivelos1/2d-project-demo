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
    [Tooltip("The total amount of times an object can be dealt 1 damage before dying.")]
    private float maximumHitPoints;

    [SerializeField]
    [Tooltip("The object's current hit points. If it dips below zero, the object dies.")]
    private float currentHitPoints;

    [SerializeField]
    [Tooltip("The object's hit points can be lowered by bullets.")]
    private bool canBeHurt;

    [SerializeField]
    [Tooltip("Multiplies the damage dealt to the object. Used for things like weakpoints or heightened defense. Default value is 1, which does not modify damage.")]
    private float damageMultiplier = 1;

    [SerializeField]

    [Tooltip("Will the object die upon reaching zero or less HP?")]
    private bool canDie;

    [SerializeField]
    [Tooltip("Gives the object X seconds before destroying itself after dying. Used to give the game time to spawn emissions or play sounds before the object is destroyed.")]
    private float destructionDelayBeforeDeath;

    [SerializeField]
    [Tooltip("The Collider2D that can be hit by bullets.")]
    private Collider2D affectedCollider;

    [SerializeField]
    [Tooltip("How long the object will stay invincible after being hit by a bullet.")]
    private float invincibilityFramesInSeconds;

    [SerializeField]
    [Tooltip("Determines how many times the object will flicker between visibility and invisibility while invincible.")]
    private float amountOfFlickersPerSecond;

    [SerializeField]
    [Tooltip("Is the object invincible?")]
    private bool invincible;

    [SerializeField]
    [Tooltip("The object will spawn these emissions upon reaching zero or less HP.")]
    private List<Emmission> emissionsOnDeath;

    private SpriteRenderer spriteRenderer;

    private float invincibilityTimer, flickerTimer;

    private Rigidbody2D rigidBody;

    private bool Dying;

    // Use this for initialization
    private void Start ()
    {
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

    private void FixedUpdate()
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
