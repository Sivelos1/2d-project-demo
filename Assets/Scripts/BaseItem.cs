using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour {
    [SerializeField]
    private PlayerCharacter user;

    [SerializeField]
    private string itemName;

    [SerializeField]
    private float coolDownTime;
    //in frames

    private float currentCoolDown;

    [SerializeField]
    private bool holdToFire = false;
    
    [SerializeField]
    private BaseBullet[] emissions;

    [SerializeField]
    private bool coolingDown = false;

    private Transform firingPoint;

	// Use this for initialization
	void Start () {
        firingPoint = user.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if(coolingDown == false)
        {
            GetInput();
        }
        else
        {
            ProgressCoolDown();
        }
    }

    private void GetInput()
    {
        if (holdToFire == true)
        {
            if (Input.GetButton("Fire1"))
            {
                Fire();
                coolingDown = true;
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
                coolingDown = true;

            }
        }
    }

    private void ProgressCoolDown()
    {
        if(currentCoolDown <= 0)
        {
            coolingDown = false;
        }
        else
        {
            coolingDown = true;
            currentCoolDown--;
        }
    }

    private void InitiateCoolDown()
    {
        coolingDown = true;
        currentCoolDown = coolDownTime;
    }

    public void Fire()
    {
        foreach (BaseBullet bullet in emissions)
        {
            Instantiate(bullet,firingPoint);
        }
        InitiateCoolDown();
    }
}
