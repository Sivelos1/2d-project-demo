using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class titleScreenController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The object used as the cursor.")]
    private GameObject cursor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
    }

    public void GoToFirstScene()
    {
        Global.SetCoins(0);
        SceneManager.LoadScene("level1");
    }
}
