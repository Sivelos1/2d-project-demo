using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

	// Use this for initialization
	void Start () {
            Debug.Log("dude fuckin stellar lol");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("Z"))
        {
            Debug.Log("nice!");
        }
	}
}
