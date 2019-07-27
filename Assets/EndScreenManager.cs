using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField]
    private string TargetScene;

    [SerializeField]
    [Tooltip("Time, in seconds, before the end screen automatically proceeds to the target scene.")]
    private float timeBeforeContinue;

    [SerializeField]
    private float screenTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        screenTime = 0;
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
