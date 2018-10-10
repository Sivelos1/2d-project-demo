using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour {

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private PlayerCharacter source;

    [SerializeField]
    private Rigidbody2D physics;

    [SerializeField]
    private BoxCollider2D collision;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float angle;

    [SerializeField]
    private float decayTime;

    [SerializeField]
    private float gravity;

    private float x_force;

    private float y_force;


	// Use this for initialization
	void Start () {
        bullet = gameObject;
        source = GetComponentInParent<PlayerCharacter>();
        physics = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        x_force = Mathf.Cos(angle) * speed;
        y_force = Mathf.Sin(angle) * speed;
        physics.AddForce(new Vector2(x_force, y_force), ForceMode2D.Impulse);
        physics.gravityScale = gravity;
        Destroy(bullet, decayTime);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

}
