using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour {

    [SerializeField]
    private bool isOnGround;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    [SerializeField]
    private ContactFilter2D groundContactFilter;

    private Collider2D[] groundCollisionResults = new Collider2D[16];

    // Use this for initialization
    void Start () {

	}

    public bool IsObjectTouchingGround()
    {
        return isOnGround;
    }
	
	// Update is called once per frame
	void Update () {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundCollisionResults) > 0;
    }
}
