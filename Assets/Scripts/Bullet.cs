using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Affects how much damage the bullet deals to an interactive object.")]
    private float baseDamage;

    [SerializeField]
    [Tooltip("How long, in seconds, the bullet will last before destroying itself if it does not collide with anything. Default value is 1 Second.")]
    private float TimeBeforeDecay = 1;

    private float decayTimer = 0;

    private Collider2D Collision;

    [SerializeField]
    private Rigidbody2D Physics;

    [SerializeField]
    [Tooltip("The bullet will destroy itself if not visible to prevent clutter.")]
    private bool destroysSelfWhenInvisible;

    [SerializeField]
    private bool HasGravity;

    [SerializeField]
    [Tooltip("The bullet will not travel through solid ground.")]
    private bool bulletCollidesWithGround;

    [Tooltip("The bullet will collide with enemies.")]
    public bool bulletCollidesWithEnemies;

    [Tooltip("The bullet will collide with miscellaneous interactive objects.")]
    public bool bulletCollidesWithProps;

    [SerializeField]
    [Tooltip("Determines whether the bullet will destroy upon collision or not.")]
    public bool bulletDisappearsUponCollision;

    [SerializeField]
    [Tooltip("When the bullet collides with something, it will spawn these objects. Think particle effects.")]
    public List<Emmission> emissionsOnDestruction;

    private SpriteRenderer sprite;

    [SerializeField]
    private bool isOnGround;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    [SerializeField]
    private ContactFilter2D groundContactFilter;

    private Collider2D[] groundCollisionResults = new Collider2D[16];

    // Use this for initialization
    private void Start ()
    {
        sprite = GetComponent<SpriteRenderer>();
        emissionsOnDestruction = new List<Emmission>();
        Physics = GetComponent<Rigidbody2D>();
        Collision = GetComponent<Collider2D>();
        if(HasGravity == true)
        {
            Physics.gravityScale = 1;
        }
        else
        {
            Physics.gravityScale = 0;
        }
    }
	
	// Update is called once per frame
	private void Update ()
    {
        decayTimer += Time.deltaTime;
        if(decayTimer > TimeBeforeDecay && TimeBeforeDecay > 0)
        {
            BulletCollision();
        }
        if (sprite.isVisible == false && destroysSelfWhenInvisible == true)
        {
            Destroy(gameObject);
        }
    }

    private void CheckForGroundCollision()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundCollisionResults) > 0;
        if(bulletCollidesWithGround == true && isOnGround == true)
        {
            BulletCollision();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Oof! Hit the ground");
            BulletCollision();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Oof! Hit an enemy");
            BulletCollision();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") && bulletCollidesWithGround == true)
        {
            BulletCollision();
        }
        if (collision.gameObject.CompareTag("Enemy") && bulletCollidesWithEnemies == true)
        {
            BulletCollision();
        }
    }

    /// <summary>
    /// BulletCollision() takes various variables surrounding the Bullet object - for example, DisappearsOnCollision - and takes them into account when the Bullet collides with an object. Then, it will trigger each (if any) emissions it is programmed to spawn upon its "death".
    /// </summary>
    private void BulletCollision()
    {
        Transform emmisionOrigin = gameObject.transform;
        if (bulletDisappearsUponCollision == true)
        {
            Physics.velocity = Vector2.zero;
            sprite.enabled = false;
            Collision.isTrigger = true;
        }
        foreach (Emmission emm in emissionsOnDestruction)
        {
            emm.GetPhysics();
            Rigidbody2D drop;
            drop = Instantiate(emm.EmissionPhysics, emmisionOrigin.position, emmisionOrigin.rotation) as Rigidbody2D;
            drop.AddForce(emm.Trajectory);
        }
        if(bulletDisappearsUponCollision == true)
        {
            Destroy(gameObject);
        }
    }

    public float GetBaseDamage()
    {
        return baseDamage;
    }





}
