using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementState
{
    NotMoving,
    StartingMovement,
    Moving,
    StoppingMovement
}

public enum MovementType
{
    Walking,
    Flying,
    Jumping
}


public class Movement : MonoBehaviour {

    [SerializeField]
    private float horizontalAcceleration;

    [SerializeField]
    private float maxHorizontalSpeed;

    [SerializeField]
    private float verticalAcceleration;

    [SerializeField]
    private float maxVerticalSpeed;

    [SerializeField]
    private float StoppingThreshold;

    private int obstacleSolvingAttempts;

    [SerializeField]
    private int MaximumObstacleSolvingAttempts;

    [SerializeField]
    private Collider2D interferenceDetector;

    [SerializeField]
    private ContactFilter2D interferenceFilter;

    private Collider2D[] interferenceResults = new Collider2D[16];

    [SerializeField]
    private PhysicsMaterial2D MovingPhysicsMaterial, StoppingPhysicsMaterial;

    private float xPosition, yPosition;
    
    private MovementState MovementState;

    private MovementType MovementType;

    private Transform ObjectTransform;

    private Direction DirectionFacing;

    private Rigidbody2D Physics;

    private float timeSpentMoving;

    private BoxCollider2D Collision;

    private GroundDetection GroundDetection;
    
    private bool FoundInterference;

    private float TargetX, TargetY;

    private float DistanceToTarget;

    Vector2 Destination;

    private MovementType previousMovementType;

    private MovementState previousMovementState;

    private float previousTargetX, previousTargetY;

    private bool PausedMovementExists;

    // Use this for initialization
    void Start () {
        ObjectTransform = GetComponent<Transform>();
        DirectionFacing = GetComponent<Direction>();
        Physics = GetComponent<Rigidbody2D>();
        Collision = GetComponent<BoxCollider2D>();
        GroundDetection = GetComponent<GroundDetection>();
    }
	
	// Update is called once per frame
	void Update () {
        DistanceToTarget =
                (Vector2.Distance(ObjectTransform.position, new Vector2(Destination.x, ObjectTransform.position.y)));
        UpdatePhysicsMaterial();
        UpdatePositionData();
	}

    void FixedUpdate()
    {
        
        if (MovementState == MovementState.Moving)
        {
            Move();
            if(CheckIfObjectIsAtTargetLocation() == true)
            {
                MovementState = MovementState.StoppingMovement;
            }

        }
        if(MovementState == MovementState.StoppingMovement)
        {
            ConcludeMovement();
        }
    }

    public void MoveTo(float coordinateX, MovementType type)
    {
        if (CheckIfPreExistingMovementIsOccuring() == true && PausedMovementExists == false && MovementState != MovementState.NotMoving)
        {
            StoreMovement();
        }
        TargetX = coordinateX;
        Destination = new Vector2(TargetX, TargetY);
        if ((Vector2.Distance(ObjectTransform.position, new Vector2(Destination.x, ObjectTransform.position.y))) <= StoppingThreshold)
        {
            //Debug.Log("Distance under Stopping Threshold. (" + (Vector2.Distance(ObjectTransform.position, new Vector2(Destination.x, ObjectTransform.position.y))) + " < " + StoppingThreshold + ")");
            return;
        }
        Debug.Log("Began movement to x:" + coordinateX +".");
        MovementState = MovementState.StartingMovement;
        MovementType = type;
        timeSpentMoving = 0;
        MovementState = MovementState.Moving;
    }

    public void MoveTo(float coordinateX, float coordinateY, MovementType type)
    {


        if (CheckIfPreExistingMovementIsOccuring() == true && PausedMovementExists == false)
        {
            StoreMovement();
        }
        TargetX = coordinateX;
        TargetY = coordinateY;
        Destination = new Vector2(TargetX, TargetY);
        if ((Vector2.Distance(ObjectTransform.position, new Vector2(Destination.x, ObjectTransform.position.y))) <= StoppingThreshold)
        {
            //Debug.Log("Distance under Stopping Threshold. ("+ (Vector2.Distance(ObjectTransform.position, new Vector2(Destination.x, ObjectTransform.position.y)))+" < "+StoppingThreshold+")");
            return;
        }

        Debug.Log("Distance to x:" + coordinateX + ", y:" + coordinateY + " from x:" + xPosition + ", y:" + yPosition + ": " + Vector2.Distance(ObjectTransform.position, Destination));
        Debug.Log("Began movement to x:"+coordinateX+", y:"+coordinateY+".");
        MovementState = MovementState.StartingMovement;
        MovementType = type;
        timeSpentMoving = 0;
        MovementState = MovementState.Moving;
    }

    public void MoveToRelativeFromPosition(float deltaX, float deltaY, MovementType type)
    {

        if (CheckIfPreExistingMovementIsOccuring() == true && PausedMovementExists == false && MovementState != MovementState.NotMoving)
        {
            StoreMovement();
        }
        TargetX = (xPosition + deltaX);
        TargetY = (yPosition + deltaY);
        Destination = new Vector2(TargetX, TargetY);
        if ((Vector2.Distance(ObjectTransform.position, new Vector2(Destination.x, ObjectTransform.position.y))) <= StoppingThreshold)
        {
            //Debug.Log("Distance under Stopping Threshold. (" + (Vector2.Distance(ObjectTransform.position, new Vector2(Destination.x, ObjectTransform.position.y))) + " < " + StoppingThreshold + ")");
            return;
        }
        Debug.Log("Distance to x:" + (xPosition + deltaX) + ", y:" + (yPosition + deltaY) + " from x:" + xPosition + ", y:" + yPosition + ": " + Vector2.Distance(ObjectTransform.position, Destination));
        Debug.Log("Began movement to x:" + (xPosition + deltaX) + ", y:" + (yPosition + deltaY) + ".");
        MovementState = MovementState.StartingMovement;
        MovementType = type;
        timeSpentMoving = 0;
        MovementState = MovementState.Moving;
    }

    private void Move()
    {
        if (DetermineIfCanGetToTarget() == false)
        {
            AttemptToBypassObstacle();
        }
        Vector2 clampedVelocity;
        switch (MovementType)
        {
            case MovementType.Walking:
                if (TargetX < xPosition)
                {
                    if(GroundDetection.IsObjectTouchingGround() == true)
                    {
                        Physics.AddForce(new Vector2(-horizontalAcceleration, 0), ForceMode2D.Impulse);
                    }
                    else
                    {
                        Physics.AddForce(new Vector2(-horizontalAcceleration, 0), ForceMode2D.Force);
                    }
                }
                else
                {
                    if (GroundDetection.IsObjectTouchingGround() == true)
                    {
                        Physics.AddForce(new Vector2(horizontalAcceleration, 0), ForceMode2D.Impulse);
                    }
                    else
                    {
                        Physics.AddForce(new Vector2(horizontalAcceleration, 0), ForceMode2D.Force);
                    }
                }
                clampedVelocity = Physics.velocity;
                clampedVelocity.x = Mathf.Clamp(Physics.velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
                Physics.velocity = clampedVelocity;
                break;
            case MovementType.Flying:
                break;

            case MovementType.Jumping:
                MovementState = MovementState.StoppingMovement;
                break;
            default:
                break;
        }
        timeSpentMoving += Time.deltaTime;

    }

    public void Jump()
    {
        if(CheckIfPreExistingMovementIsOccuring() == true && PausedMovementExists == false)
        {
            StoreMovement();
        }
        MovementState = MovementState.StartingMovement;
        MovementType = MovementType.Jumping;
        if (GroundDetection.IsObjectTouchingGround() == true)
        {
            Physics.AddForce(Vector2.up * verticalAcceleration, ForceMode2D.Impulse);
            MovementState = MovementState.Moving;
        }
        
    }

    private bool CheckIfObjectIsAtTargetLocation()
    {
        switch (MovementType)
        {
            case MovementType.Walking:
                if ((Vector2.Distance(ObjectTransform.position, new Vector2(Destination.x, ObjectTransform.position.y))) <= StoppingThreshold && GroundDetection.IsObjectTouchingGround() == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case MovementType.Flying:
                return true;
            case MovementType.Jumping:
                if(GroundDetection.IsObjectTouchingGround() == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return true;
        }
    }

    private void ConcludeMovement()
    {
        switch (MovementType)
        {
            case MovementType.Walking:
                if (GroundDetection.IsObjectTouchingGround() == true && DistanceToTarget <= StoppingThreshold)
                {
                    MovementState = MovementState.NotMoving;
                }
                break;
            case MovementType.Flying:
                break;
            case MovementType.Jumping:
                if (GroundDetection.IsObjectTouchingGround() == true)
                {
                    MovementState = MovementState.NotMoving;
                }
                break;
            default:
                MovementState = MovementState.NotMoving;
                break;
        }
        Debug.Log("Finished moving. Time elapsed: "+timeSpentMoving+" seconds.");
        if(PausedMovementExists == true)
        {
            ResumePausedMovement();
        }
    }

    private void UpdatePhysicsMaterial()
    {
        if (Mathf.Abs(Physics.velocity.x) > 0)
        {
            // TODO moving physics material
            Collision.sharedMaterial = MovingPhysicsMaterial;
        }
        else
        {
            //TODO stopping physics material
            Collision.sharedMaterial = StoppingPhysicsMaterial;

        }
        Physics.sharedMaterial = Collision.sharedMaterial;
    }

    private void UpdatePositionData()
    {
        ObjectTransform = gameObject.transform;
        xPosition = gameObject.transform.position.x;
        yPosition = gameObject.transform.position.y;
    }

    public bool DetermineIfCanGetToTarget()
    {
        if (interferenceDetector.OverlapCollider(interferenceFilter, interferenceResults) > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void AttemptToBypassObstacle()
    {
        switch (MovementType)
        {
            case MovementType.Walking:
                Jump();
                obstacleSolvingAttempts++;
                break;
            case MovementType.Flying:
                break;
            default:
                break;
        }
        if(obstacleSolvingAttempts > MaximumObstacleSolvingAttempts)
        {
            PausedMovementExists = false;
            MovementState = MovementState.StoppingMovement;
            Debug.Log("Cannot get to obstacle.");
        }
    }

    public bool IsMoving()
    {
        if(MovementState == MovementState.Moving)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckIfPreExistingMovementIsOccuring()
    {
        if(MovementState == MovementState.Moving)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StoreMovement()
    {
        previousMovementType = MovementType;
        previousMovementState = MovementState;
        previousTargetX = TargetX;
        previousTargetY = TargetY;
        Debug.Log("Paused " + previousMovementType + " to x:" + previousTargetX + ", y:" + previousTargetY + ".");
        PausedMovementExists = true;
    }

    private void ResumePausedMovement()
    {
        TargetX = previousTargetX;
        TargetY = previousTargetY;
        MovementType = previousMovementType;
        Debug.Log("Resuming " + previousMovementType + " to x:" + previousTargetX + ", y:" + previousTargetY + ".");
        MovementState = previousMovementState;
        PausedMovementExists = false;
    }
}
