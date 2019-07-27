using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The player character the UI is bound to.")]
    private PlayerCharacter player;

    [SerializeField]
    private int coins
    {
        get
        {
            int i = player.GetCoin();
            if(i < 0)
            {
                return 0;
            }
            else if(i > 999999)
            {
                return 999999;
            }
            else
            {
                return i;
            }
        }
    }

    [SerializeField]
    private float time
    {
        get
        {
            return player.GetTimer();
        }
    }

    [SerializeField]
    [Tooltip("The Text to draw the player's coins to.")]
    private Text coinDisplay;

    [SerializeField]
    [Tooltip("The Text to draw the game's timer to.")]
    private Text timeDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (coinDisplay)
            {
                coinDisplay.text = coins.ToString();
            }
            if (timeDisplay)
            {
                //https://answers.unity.com/questions/1476208/string-format-to-show-float-as-time.html
                //Thanks to this guy for the help
                int minutes = (int)time / 60;
                int seconds = (int)time - 60 * minutes;
                if (seconds == 60 || (seconds%60 == 0 && seconds != 0))
                {
                    minutes++;
                }
                timeDisplay.text = (minutes+":"+seconds.ToString("00"));
            }
        }
    }
}
