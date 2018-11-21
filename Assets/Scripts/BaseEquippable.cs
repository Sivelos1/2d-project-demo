using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Primary,
    Secondary
}

public class BaseEquippable : MonoBehaviour {
    [SerializeField]
    private string name;

    [SerializeField]
    private bool onlyTriggersOnButtonDown = true;

    private bool fire;

    [SerializeField]
    private WeaponType weaponType;

    [SerializeField]
    private PlayerCharacter user;

    [SerializeField]
    private List<GameObject> emissions = new List<GameObject>();

	// Use this for initialization
	void Start () {
        user = GetComponentInParent<PlayerCharacter>();
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
	}

    private void FixedUpdate()
    {
        if(fire == true)
        {
            Fire();
        }
    }

    public void GetInput()
    {
        if(onlyTriggersOnButtonDown == true)
        {
            fire = Input.GetButtonDown("Fire1");
        }
        else
        {
            fire = Input.GetButton("Fire1");
        }
    }

    public void Fire()
    {
        Debug.Log("Firing!");
        foreach (GameObject g in emissions)
        {
            Instantiate(g);
            g.transform.position = gameObject.transform.position;
            
        }

    }
}
