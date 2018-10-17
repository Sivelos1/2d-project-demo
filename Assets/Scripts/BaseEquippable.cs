using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEquippable : MonoBehaviour {
    [SerializeField]
    public string name;

    [SerializeField]
    public bool onlyTriggersOnFirstFrame = true;

    [SerializeField]
    public PlayerCharacter user;

    private List<Effect> effects = new List<Effect>();

	// Use this for initialization
	void Start () {
        user = GetComponentInParent<PlayerCharacter>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        
    }

    public void Fire()
    {
        Debug.Log("Firing!");
        foreach (Effect e in effects)
        {
            e.Trigger();
        }
    }
}
