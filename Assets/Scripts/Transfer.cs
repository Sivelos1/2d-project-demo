using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transfer : MonoBehaviour {

    [SerializeField]
    string TargetScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("The player activated the door.");
            SceneManager.LoadScene(TargetScene);
        }
    }
}
