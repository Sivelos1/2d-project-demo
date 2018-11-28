using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour {

    public enum Direction
    {
        left,
        right
    }

    [SerializeField]
    private string Name = "NPC";

    [SerializeField]
    public int HealthPoints = 3;

    [SerializeField]
    private int MaxHealth = 3;

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

    [SerializeField]
    private Collider2D Detection;

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
    private bool CanBeHurtByPlayer;

    [SerializeField]
    private float maxSpeed = 5;


    private SpriteRenderer spriteRenderer;

    private Animator animator;




    // Use this for initialization
    private void Start()
    {
        HealthPoints = MaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidBody2DInstance = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        Detection = GetComponentInChildren<Collider2D>();
        if (Detection != null)
        {
            Debug.Log("We have eyesight lads");
        }
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
    }
    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundCollisionResults) > 0;
    }

    private void SyncUpAnimations()
    {
        
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player spotted!");
        }

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

    private void TurnSprite()
    {
        if (IsFacingLeft() == true)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void Move(Direction direction, float time)
    {
        
        float movementTimer = 0;
        directionFacing = direction;
        TurnSprite();
        collision.sharedMaterial = playerMovingPhysicsMaterial;
        while (movementTimer < time)
        {
            if (isOnGround == false)
            {
                rigidBody2DInstance = GetComponent<Rigidbody2D>();
                rigidBody2DInstance.AddForce(new Vector2((horizontalAcceleration), 0), ForceMode2D.Force);
            }
            else
            {
                rigidBody2DInstance = GetComponent<Rigidbody2D>();
                rigidBody2DInstance.AddForce(new Vector2((horizontalAcceleration), 0), ForceMode2D.Impulse);
            }
            Vector2 clampedVelocity = rigidBody2DInstance.velocity;
            clampedVelocity.x = Mathf.Clamp(rigidBody2DInstance.velocity.x, -maxSpeed, maxSpeed);
            rigidBody2DInstance.velocity = clampedVelocity;
            movementTimer += Time.deltaTime;
        }
        Debug.Log("Moving complete");
        collision.sharedMaterial = playerStoppingPhysicsMaterial;

    }

    public void Jump()
    {
        if(isOnGround == true)
        {
            rigidBody2DInstance.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
        
    }

    //private void Move()
    //{
    //    if (IsFacingLeft() == true)
    //    {
    //        gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
    //    }
    //    else
    //    {
    //        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    //    }
    //    if (isOnGround == false)
    //    {
    //        rigidBody2DInstance.AddForce(new Vector2((horizontalAcceleration * horizontalInput), 0), ForceMode2D.Force);
    //    }
    //    else
    //    {
    //        rigidBody2DInstance.AddForce(new Vector2((horizontalAcceleration * horizontalInput), 0), ForceMode2D.Force);
    //    }
    //    Vector2 clampedVelocity = rigidBody2DInstance.velocity;
    //    clampedVelocity.x = Mathf.Clamp(rigidBody2DInstance.velocity.x, -maxSpeed, maxSpeed);
    //    rigidBody2DInstance.velocity = clampedVelocity;
    //}
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

    public bool IsOnGround()
    {
        return isOnGround;
    }


}
