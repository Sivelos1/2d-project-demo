using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicController : MonoBehaviour
{
    //Source:
    //https://www.youtube.com/watch?v=JKoBWBXVvKY

    [SerializeField]
    [Tooltip("The background music will not play in these scenes.")]
    private List<string> bannedScenes;

    // Start is called before the first frame update
    void Awake()
    {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("bgm");
            if (objs.Length > 1)
                Destroy(this.gameObject);

            DontDestroyOnLoad(this.gameObject);        
    }

    void Update()
    {
        if (bannedScenes.Contains(SceneManager.GetActiveScene().name))
        {
            Destroy(this.gameObject);
        }
    }
}
