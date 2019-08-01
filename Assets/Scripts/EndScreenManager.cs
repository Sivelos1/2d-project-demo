using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField]
    private string TargetScene;

    [SerializeField]
    [Tooltip("Time, in seconds, before the end screen automatically proceeds to the target scene.")]
    private float timeBeforeContinue;

    [SerializeField]
    [Tooltip("The sound to play on entering the scene.")]
    private AudioClip stinger;

    [SerializeField]
    [Tooltip("The string to display when the game ends.")]
    private string gameOverText;

    [SerializeField]
    [Tooltip("The string to display when the timer reaches 0.")]
    private string timeOutText;

    [SerializeField]
    private Text textField;

    [SerializeField]
    private float screenTime = 0;

    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "gameOver")
        {
            if(Global.GetTimer() <= 0)
            {
                textField.text = timeOutText;
            }
            else
            {
                textField.text = gameOverText;
            }
        }
        sound = GetComponent<AudioSource>();
        screenTime = 0;
        if(sound && stinger)
        {
            sound.clip = stinger;
            sound.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        screenTime += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") || screenTime >= timeBeforeContinue)
        {
            if (TargetScene == null || TargetScene == "")
            {
                Debug.Log("This transfer has no target.");
                return;
            }
            SceneManager.LoadScene(TargetScene);

        }
    }
}
