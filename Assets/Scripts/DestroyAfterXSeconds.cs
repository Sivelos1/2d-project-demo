using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Waits X seconds before destroying the object.")]
    private float destroyDelay;
    // Use this for initialization
    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
