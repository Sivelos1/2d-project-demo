﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class titleScreenController : MonoBehaviour, IMoveHandler
{
    [SerializeField]
    [Tooltip("The canvas the intro is bound to.")]
    private GameObject introCanvas;

    [SerializeField]
    [Tooltip("The text field to write the intro's text blurbs to.")]
    private Text textField;

    [SerializeField]
    [Tooltip("The SpriteRenderer that renders the intro slides.")]
    private SpriteRenderer introSlide;

    [SerializeField]
    [Tooltip("The blurbs of text, in numerical order, displayed by the intro.")]
    private List<string> introText = new List<string>();

    [SerializeField]
    [Tooltip("The pictures displayed during the intro, in numerical order.")]
    private List<Sprite> introPictures = new List<Sprite>();

    [SerializeField]
    [Tooltip("The index of the currently selected intro slide.")]
    private int introIndex;

    [SerializeField]
    [Tooltip("The amount of time, in seconds, before the next slide of the intro pops up.")]
    private float introTransitionDelay;

    [SerializeField]
    [Tooltip("The current time of the intro transitioning.")]
    private float introTransitionTimer = 0;

    [SerializeField]
    [Tooltip("Is the intro playing?")]
    private bool isInIntro = false;

    [SerializeField]
    [Tooltip("The amount of time, in seconds, the game waits to display the intro if no input is made during that time.")]
    private float timeBeforeIntro;

    [SerializeField]
    [Tooltip("If this value exceeds or matches Time Before Intro, the intro plays. Set to 0 by any input.")]
    private float currentIntroTime = 0;

    [SerializeField]
    [Tooltip("The names of the button inputs to check for.")]
    private List<string> buttonsToCheck = new List<string>();

    [SerializeField]
    [Tooltip("The names of the Axis inputs to check for.")]
    private List<string> axesToCheck = new List<string>();

    [SerializeField]
    [Tooltip("The AudioSource playing the title BGM.")]
    private AudioSource sound;

    [SerializeField]
    private bool debug;

    void Start()
    {
        introIndex = 0;
        sound = GetComponent<AudioSource>();
        currentIntroTime = 0;
        introCanvas.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        
        currentIntroTime += Time.deltaTime;
        if (CheckForInput())
        {
            if (isInIntro)
            {
                EndIntro();
            }
            else
            {
                currentIntroTime = 0;
            }
        }
        if (Mathf.Round(currentIntroTime) == timeBeforeIntro)
        {
            StartIntro();
        }
        else if(Mathf.Round(currentIntroTime) > timeBeforeIntro)
        {
            
            introTransitionTimer += Time.deltaTime;
            textField.text = introText[introIndex];
            introSlide.sprite = introPictures[introIndex];
            if (Mathf.Round(introTransitionTimer) >= introTransitionDelay)
            {
                introIndex++;
                if (introIndex >= introPictures.Count)
                {
                    EndIntro();
                }
                introTransitionTimer = 0;
            }
        }
        
    }

    private void DebugPrint(string message)
    {
        if (debug)
        {
            Debug.Log(message);
        }
    }

    private bool CheckForInput()
    {
        DebugPrint("Beginning input check.");
        DebugPrint("Checking for input in the buttons... "+buttonsToCheck.Count+" buttons found.");
        for (int i = 0; i < buttonsToCheck.Count; i++)
        {
            DebugPrint("Checking for input on button "+buttonsToCheck[i]+"...");
            if (Input.GetButton(buttonsToCheck[i]))
            {
                DebugPrint("Input on button "+buttonsToCheck[i]+" found!");
                return true;
            }
        }
        DebugPrint("Checking for input in the axes... " + axesToCheck.Count + " axes found.");
        for (int i = 0; i < axesToCheck.Count; i++)
        {
            DebugPrint("Checking for input on axis " + axesToCheck[i] + "...");
            if (Input.GetAxisRaw(axesToCheck[i]) != 0)
            {
                DebugPrint("Input on axis " + axesToCheck[i] + " found!");
                return true;
            }
        }
        return false;
    }

    public void GoToFirstScene()
    {
        if(isInIntro)
        {
            EndIntro();
        }
        else
        {
            Global.SetCoins(0);
            Global.SetTimer(120);
            SceneManager.LoadScene("level1");
        }
    }

    public void StartIntro()
    {
        isInIntro = true;
        introCanvas.SetActive(true);
        textField.text = introText[0];
        introSlide.sprite = introPictures[0];

    }

    public void EndIntro()
    {
        if (sound && !sound.isPlaying)
        {
            sound.Play();
        }
        isInIntro = false;
        introIndex = 0;
        introTransitionTimer = 0;
        currentIntroTime = 0;
        introCanvas.SetActive(false);
    }
    
    public void OnMove(AxisEventData eventData)
    {
        if (isInIntro)
        {
            EndIntro();
        }
    }
}
