using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private PlayerCharacter user;

    private BaseEquippable Spawner;

    [SerializeField]
    private float Angle;

    [SerializeField]
    private float Speed;

    [SerializeField]
    private float TimeBeforeDecay = 1;

    private float decayTimer = 0;

    private Collider2D Collision;

    private Rigidbody2D Physics;

	// Use this for initialization
	void Start () {
        user = GetComponentInParent<PlayerCharacter>();
        Spawner = GetComponentInParent<BaseEquippable>();
        Physics = GetComponent<Rigidbody2D>();
        Collision = GetComponent<Collider2D>();
        if (user.IsFacingLeft() == true)
        {
            Physics.AddForce(new Vector2(-(Mathf.Cos(Angle) * Speed), (Mathf.Sin(Angle) * Speed)));

        }
        else
        {
            Physics.AddForce(new Vector2((Mathf.Cos(Angle) * Speed), (Mathf.Sin(Angle) * Speed)));
        }
    }
	
	// Update is called once per frame
	void Update () {
        decayTimer += Time.deltaTime;
        if(decayTimer > TimeBeforeDecay && TimeBeforeDecay > 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Oof! The enemy was hit by the bullet.");
        }
    }

    public void GetUser(PlayerCharacter Source)
    {
        user = Source;
    }

}
