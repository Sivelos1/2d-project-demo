using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField]
    private float horizontalSpeed;

    [SerializeField]
    private float verticalSpeed;

    [SerializeField]
    private PhysicsMaterial2D MovingPhysicsMaterial, StoppingPhysicsMaterial;

    private Transform ObjectTransform;

    private Direction DirectionFacing;

    private Rigidbody2D Physics;

    private BoxCollider2D Collision;

    private GroundDetection GroundDetection;

	// Use this for initialization
	void Start () {
        ObjectTransform = GetComponent<Transform>();
        DirectionFacing = GetComponent<Direction>();
        Physics = GetComponent<Rigidbody2D>();
        Collision = GetComponent<BoxCollider2D>();
        GroundDetection = GetComponent<GroundDetection>();
        MoveTo(ObjectTransform.position.x + 5, ObjectTransform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {

    }

    public void MoveTo(float coordinateX, float coordinateY)
    {
        float timeSpentMoving = 0;
        Debug.Log("Began moving to "+coordinateX+"/"+coordinateY+".");
        while (ObjectTransform.position.x != coordinateX && ObjectTransform.position.y != coordinateY)
        {
            Physics.AddForce(new Vector2(horizontalSpeed, 0), ForceMode2D.Force);
            Vector2 clampedVelocity = Physics.velocity;
            clampedVelocity.x = Mathf.Clamp(Physics.velocity.x, -horizontalSpeed, horizontalSpeed);
            Physics.velocity = clampedVelocity;
            timeSpentMoving += Time.deltaTime;
        }
        Debug.Log("Moved to " + coordinateX + "/" + coordinateY + " in "+timeSpentMoving+" seconds.");
    }

    public void Jump()
    {
        if(GroundDetection.IsObjectTouchingGround() == true)
            Physics.AddForce(Vector2.up * verticalSpeed, ForceMode2D.Impulse);
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
    }
}
