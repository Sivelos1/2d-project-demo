﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour {

    [SerializeField]
    private bool CheckPointActivated = false;

    [SerializeField]
    private AudioClip onActivationSound;

    private AudioSource sound;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(CheckPointActivated == true)
            {
                Debug.Log("This checkpoint is already activated.");
            }
            else
            {
                if (sound)
                {
                    sound.clip = onActivationSound;
                    sound.Play();
                }
                Debug.Log("The player activated the checkpoint!");
                PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
                player.SetCurrentCheckpoint(this);
                CheckPointActivated = true;
                animator.SetBool("Activated", CheckPointActivated);
            }
            
        }
    }
}
