using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private string name = "Mario";

    [SerializeField]
    private float jumpHeight = 5, speed = 5;

    private bool jumping;

    [SerializeField]
    private bool isOnGround;

    private Rigidbody2D rigidBody2DInstance;
    private BoxCollider2D collision;


    // Use this for initialization
    private void Start () {
        rigidBody2DInstance = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        Debug.Log("dude fuckin stellar lol");


    }
	
	// Update is called once per frame
	private void Update () {

        if (Input.GetButton("Jump"))
        {
            Move(0, 5);
        }
    }

    private void Move(float changeToX, float changeToY)
    {
        float x = rigidBody2DInstance.velocity.x;
        float y = rigidBody2DInstance.velocity.y;
        rigidBody2DInstance.velocity = new Vector2(x + changeToX, y + changeToY);
    }

}
