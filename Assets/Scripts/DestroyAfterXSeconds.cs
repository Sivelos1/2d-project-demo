using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour {
    [SerializeField]
    private float destroyDelay;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
