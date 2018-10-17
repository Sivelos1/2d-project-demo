using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Affects
{
    None,
    Target,
    Source
}

public class Effect : MonoBehaviour {

    [SerializeField]
    private Affects Affects = Affects.None;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        
    }

    public void Trigger()
    {

    }
}
