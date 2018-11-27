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
    public Transform bulletOrigin;

    [SerializeField]
    private bool onlyTriggersOnButtonDown = true;

    private bool fire;

    [SerializeField]
    private WeaponType weaponType;

    [SerializeField]
    private PlayerCharacter user;

    [SerializeField]
    private List<Emmission> emissions = new List<Emmission>();

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
        AlignWeapon();
        GetInput();
        if (fire == true)
        {
            Fire();
        }
    }

    private void AlignWeapon()
    {
        if(user.IsFacingLeft() == true)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
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
        foreach (Emmission g in emissions)
        {
            g.GetPhysics();
            Rigidbody2D bullet;
            bullet = Instantiate(g.EmissionPhysics, bulletOrigin.position, bulletOrigin.rotation) as Rigidbody2D;
            if (user.IsFacingLeft() && g.IgnoreWeaponRotation == false)
            {
                bullet.AddForce(-g.Trajectory);
            }
            else
            {
                bullet.AddForce(g.Trajectory);
            }
            
        }

    }

    public PlayerCharacter GetUser()
    {
        return user;
    }
}
