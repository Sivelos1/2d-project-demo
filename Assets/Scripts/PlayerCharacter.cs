using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour {
    private enum Direction
    {
        left,
        right
    }

    [SerializeField]
    private int health = 3;

    [SerializeField]
    private int Coins = 0;

    [SerializeField]
    private bool IsDead = false;

    [SerializeField]
    private Direction directionFacing = Direction.right;

    [SerializeField]
    private float jumpHeight = 10, speed = 5;

    private bool jumping;

    [SerializeField]
    private bool isOnGround;

    private Rigidbody2D rigidBody2DInstance;

    private SpriteRenderer sprite;

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

    [SerializeField]
    private float RespawnDelay = 1;

    private float respawnTimer = 0;

    private SpriteRenderer spriteRenderer;

    private Animator animator;

    private Checkpoint currentCheckPoint;

    private bool Dying = false;

    private Transfer targetDoor;

    private bool EnteringDoor;

    private AudioSource sound;

    [SerializeField]
    private AudioClip jumpSound, deathSound, hurtSound;

    private bool DisableInput = false;




    // Use this for initialization
    private void Start () {
        sound = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidBody2DInstance = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        equip = new BaseEquippable();
        Debug.Log("dude fuckin stellar lol");
    }
    // Update is called once per frame
    private void Update() {
        if (EnteringDoor == true)
        {
            if (IsFacingLeft() == true)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            if (IsDead == true)
                return;
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
        else
        {
            UpdateIsOnGround();
            if(DisableInput == false)
            {
                GetInput();
                HandleJumpInput();
            }
            SyncUpAnimations();
            if (IsDead == true)
            {

                Death();
            }
        }
        

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
        animator.SetBool("GoingIntoDoor", EnteringDoor);
        animator.SetBool("Dead", IsDead);
        animator.SetBool("PressingMoveButton", (Mathf.Abs(Input.GetAxisRaw("Horizontal")) != 0));
        animator.SetBool("TouchingGround", isOnGround);
        animator.SetFloat("Y_Speed", rigidBody2DInstance.velocity.y);
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
        if (IsDead == true)
            return;
        if (Input.GetButtonDown("Jump") && isOnGround == true)
        {
            sound.clip = jumpSound;
            sound.Play();
            rigidBody2DInstance.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

    }
    private void GetInput()
    {
        if (IsDead == true)
            return;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        
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
        if (IsDead == true)
            return;
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

    private void Death()
    {
        if(Dying == false)
        {
            sound.clip = deathSound;
            sound.Play();
            rigidBody2DInstance.AddForce(new Vector2(-rigidBody2DInstance.velocity.x, -rigidBody2DInstance.velocity.y), ForceMode2D.Impulse);
            collision.isTrigger = true;
            if (directionFacing == Direction.left)
            {
                rigidBody2DInstance.AddForce(new Vector2(5, 10), ForceMode2D.Impulse);
                
            }
            else
            {
                rigidBody2DInstance.AddForce(new Vector2(-5, 10), ForceMode2D.Impulse);
                
            }
            collision.isTrigger = false;
            Dying = true;

        }
        else
        {
            if(isOnGround == true)
            {
                
                respawnTimer += Time.deltaTime;
                if (isOnGround && respawnTimer >= RespawnDelay)
                {
                    Respawn();
                }
            }
            
        }
        
    }

    public void Die()
    {
        IsDead = true;
    }

    public void Respawn()
    {
        IsDead = false;
        Dying = false;
        respawnTimer = 0;
        if (currentCheckPoint == null)
        {
            SceneManager.LoadScene("gameOver");
        }
        else
        {
            rigidBody2DInstance.velocity = Vector2.zero;
            transform.position = currentCheckPoint.transform.position;
            SyncUpAnimations();
            
        }
        
    }
    public bool GetIsDeadValue()
    {
        return IsDead;
    }
    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        currentCheckPoint = newCurrentCheckpoint;
    }

    public void GetCoin(int amount)
    {
        Coins += amount;
    }

    public void GoIntoDoor(Transfer target)
    {
        targetDoor = target;
        EnteringDoor = true;
    }

    public void Hide()
    {
        spriteRenderer.enabled = false;
    }

    public void Show()
    {
        spriteRenderer.enabled = true;
    }

    public bool AreControlsDisabled()
    {
        return DisableInput;
    }

    public void ToggleInputControl()
    {
        DisableInput = !DisableInput;
    }

}
