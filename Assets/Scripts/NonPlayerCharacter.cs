using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour {

    private enum Direction
    {
        left,
        right
    }

    [SerializeField]
    private string Name = "NPC";

    [SerializeField]
    private int HealthPoints = 3;

    [SerializeField]
    private Direction directionFacing = Direction.right;

    [SerializeField]
    private float jumpHeight = 10, speed = 5;

    private bool jumping;

    [SerializeField]
    private bool isOnGround;

    private Rigidbody2D rigidBody2DInstance;

    [SerializeField]
    private BoxCollider2D collision;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    [SerializeField]
    private ContactFilter2D groundContactFilter;

    private Collider2D[] groundCollisionResults = new Collider2D[16];

    private float horizontalInput;
    private float verticalInput;

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    [SerializeField]
    private float horizontalAcceleration = 5;
    [SerializeField]
    private float verticalAcceleration = 20;

    [SerializeField]
    private float maxSpeed = 5;


    private SpriteRenderer spriteRenderer;

    private Animator animator;




    // Use this for initialization
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidBody2DInstance = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        Debug.Log("enemy IS HERE");
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateIsOnGround();
        SyncUpAnimations();

    }
    private void FixedUpdate()
    {
        UpdatePhysicsMaterial();
        UpdateDirectionFacing();
        Move();
    }
    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundCollisionResults) > 0;
    }

    private void SyncUpAnimations()
    {
        
    }

    private void UpdateDirectionFacing()
    {
        if (horizontalInput > 0)
        {
            directionFacing = Direction.right;
        }
        else if (horizontalInput < 0)
        {
            directionFacing = Direction.left;
        }
    }
    private void UpdatePhysicsMaterial()
    {
        if (Mathf.Abs(rigidBody2DInstance.velocity.x) > 0)
        {
            // TODO moving physics material
            collision.sharedMaterial = playerMovingPhysicsMaterial;
        }
        else
        {
            //TODO stopping physics material
            collision.sharedMaterial = playerStoppingPhysicsMaterial;

        }
    }

    private void Move()
    {
        if (IsFacingLeft() == true)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        if (isOnGround == false)
        {
            rigidBody2DInstance.AddForce(new Vector2((horizontalAcceleration * horizontalInput), 0), ForceMode2D.Force);
        }
        else
        {
            rigidBody2DInstance.AddForce(new Vector2((horizontalAcceleration * horizontalInput), 0), ForceMode2D.Force);
        }
        Vector2 clampedVelocity = rigidBody2DInstance.velocity;
        clampedVelocity.x = Mathf.Clamp(rigidBody2DInstance.velocity.x, -maxSpeed, maxSpeed);
        rigidBody2DInstance.velocity = clampedVelocity;
    }
    public bool IsFacingLeft()
    {
        if (directionFacing == Direction.left)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
