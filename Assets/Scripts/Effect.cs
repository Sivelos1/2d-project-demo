using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Affects
{
    None,
    Target,
    Source
}

public enum FiringCondition
{
    WhenFiring,
    Always
}

public class Effect : MonoBehaviour {

    [SerializeField]
    private Affects Affects = Affects.None;

    [SerializeField]
    private FiringCondition FiringCondition = FiringCondition.WhenFiring;

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
