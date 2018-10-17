using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
    private enum Direction
    {
        left,
        right
    }

    [SerializeField]
    private int lives = 3;

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

    [SerializeField]
    private BaseEquippable equip;

    private bool holdingFireButton;

    private SpriteRenderer spriteRenderer;

    private Animator animator;
    



    // Use this for initialization
    private void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2DInstance = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        equip = new BaseEquippable();
        Debug.Log("dude fuckin stellar lol");
    }
	
	// Update is called once per frame
	private void Update () {

        UpdateIsOnGround();
        GetInput();
        HandleJumpInput();

    }
    private void FixedUpdate()
    {
        UpdatePhysicsMaterial();
        UpdateDirectionFacing();
        ActivateEquipment();
        Move();
    }
    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundCollisionResults) > 0;
    }

    private void UpdateDirectionFacing()
    {
        if(horizontalInput > 0)
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
        if(Mathf.Abs(horizontalInput) > 0)
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
    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isOnGround == true)
        {
            rigidBody2DInstance.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if(equip != null)
        {
            if (equip.onlyTriggersOnFirstFrame == false)
            {
                holdingFireButton = Input.GetButton("Fire1");
            }
            else
            {
                holdingFireButton = Input.GetButtonDown("Fire1");
            }
        }
        else
        {
            holdingFireButton = Input.GetButtonDown("Fire1");
        }
        
    }
    private void Move()
    {
        if(IsFacingLeft() == true)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        if(isOnGround == false)
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
    private void ActivateEquipment()
    {
        if(holdingFireButton == true)
        {
            if(equip != null)
            {
                equip.Fire();
            }
            else
            {
                Debug.Log("no fuckin weapon lol");
            }
        }
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
