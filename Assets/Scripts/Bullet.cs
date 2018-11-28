using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    private float TimeBeforeDecay = 1;

    private float decayTimer = 0;

    private Collider2D Collision;

    [SerializeField]
    private Rigidbody2D Physics;

    [SerializeField]
    private bool HasGravity;

    [SerializeField]
    public bool DisappearsOnCollision;

	// Use this for initialization
	void Start () {
        Physics = GetComponent<Rigidbody2D>();
        Collision = GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        decayTimer += Time.deltaTime;
        if(decayTimer > TimeBeforeDecay && TimeBeforeDecay > 0)
        {
            Destroy(gameObject);
        }
    }
    




}
