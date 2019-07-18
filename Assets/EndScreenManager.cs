using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField]
    private string TargetScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
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
