using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour {

    [SerializeField]
    private int Worth = 1;

    private AudioSource sound;

    private PlayerCharacter user;


    // Use this for initialization
    void Start () {
        sound = GetComponent<AudioSource>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            user = collision.gameObject.GetComponent<PlayerCharacter>();
            user.GetCoin(Worth);
            sound.Play();
            Destroy(gameObject);
        }
    }

}
