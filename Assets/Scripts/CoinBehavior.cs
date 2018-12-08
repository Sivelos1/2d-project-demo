using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Gives the player X points upon collecting this coin. Default value is 1.")]
    private int Worth = 1;

    [SerializeField]
    [Tooltip("Gives the coin gravity, instead of floaing in mid-air.")]
    private bool HasGravity;

    private Rigidbody2D physics;

    private AudioSource sound;

    private PlayerCharacter user;

    private bool GotCoin;

    private SpriteRenderer sprite;

    private BoxCollider2D boxCollider;

    // Use this for initialization
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        GotCoin = false;
        physics = GetComponent<Rigidbody2D>();
        if (HasGravity == false)
        {
            physics.gravityScale = 0;
        }
        else
        {
            physics.gravityScale = 1;
        }
        sound = GetComponent<AudioSource>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(GotCoin == false)
            {
                Debug.Log("Ding! The player picked up " + Worth + " coin(s)!");
                sound.Play();
                sprite.enabled = false;
                boxCollider.isTrigger = true;
                GotCoin = true;
                Destroy(gameObject, sound.clip.length);
            }
        }
    }
}
