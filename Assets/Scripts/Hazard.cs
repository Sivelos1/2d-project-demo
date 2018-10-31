using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("The player has touched the hazard");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            player.Die();
        }
        else
        {
            Debug.Log("Something touched the hazard");
        }
    }
}
