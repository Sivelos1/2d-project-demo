using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private float baseDamage;

    [SerializeField]
    private float TimeBeforeDecay = 1;

    private float decayTimer = 0;

    private Collider2D Collision;

    [SerializeField]
    private Rigidbody2D Physics;

    [SerializeField]
    private bool DestroyWhenOffScreen;

    [SerializeField]
    private bool HasGravity;

    [SerializeField]
    private bool collidesWithGround;

    public bool collidesWithEnemies;

    public bool collidesWithProps;

    [SerializeField]
    public bool DisappearsOnCollision;

    [SerializeField]
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
    void Start () {
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
	void Update () {
        decayTimer += Time.deltaTime;
        if(decayTimer > TimeBeforeDecay && TimeBeforeDecay > 0)
        {
            BulletCollision();
        }
        if (sprite.isVisible == false && DestroyWhenOffScreen == true)
        {
            Debug.Log("so long gay bowser");
            Destroy(gameObject);
        }
    }

    private void CheckForGroundCollision()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundCollisionResults) > 0;
        if(collidesWithGround == true && isOnGround == true)
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
        if (collision.gameObject.CompareTag("Floor") && collidesWithGround == true)
        {
            BulletCollision();
        }
        if (collision.gameObject.CompareTag("Enemy") && collidesWithEnemies == true)
        {
            BulletCollision();
        }
    }

    private void BulletCollision()
    {
        Transform emmisionOrigin = gameObject.transform;
        if (DisappearsOnCollision == true)
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
        if(DisappearsOnCollision == true)
        {
            Destroy(gameObject);
        }
    }

    public float GetBaseDamage()
    {
        return baseDamage;
    }





}
