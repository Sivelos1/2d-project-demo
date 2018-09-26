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

    private float horizontalInput;
    private float verticalInput;


    // Use this for initialization
    private void Start () {
        rigidBody2DInstance = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        Debug.Log("dude fuckin stellar lol");


    }
	
	// Update is called once per frame
	private void Update () {

        GetInput();
        Move();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
}

    private void Move()
    {
        rigidBody2DInstance.velocity = new Vector2(horizontalInput, 0);
    }

}
