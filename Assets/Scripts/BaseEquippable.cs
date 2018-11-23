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
    private string name = "N/A";

    [SerializeField]
    private string InputKey = "N/A";

    [SerializeField]
    private bool onlyTriggersOnButtonDown = true;

    private bool fire;

    [SerializeField]
    private WeaponType weaponType;

    [SerializeField]
    private PlayerCharacter user;

    [SerializeField]
    private List<Bullet> emissions = new List<Bullet>();

	// Use this for initialization
	void Start () {
        user = GetComponentInParent<PlayerCharacter>();
        if(user != null)
        {
            Debug.Log(name + " has a parent user.");
        }
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
        if (fire == true)
        {
            Fire();
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void GetInput()
    {
        if(onlyTriggersOnButtonDown == true)
        {
            fire = Input.GetButtonDown(InputKey);
        }
        else
        {
            fire = Input.GetButton(InputKey);
        }
    }

    public void Fire()
    {
        Debug.Log("Firing!");
        foreach (Bullet g in emissions)
        {
            Instantiate(g);
            g.transform.position = user.transform.position;
            
        }

    }
}
