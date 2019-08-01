using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Primary,
    Secondary
}
public class BaseEquippable : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The weapon's name as shown to the player.")]
    private string name = "N/A";

    [SerializeField]
    [Tooltip("The input used to activate the equipment.")]
    private string InputKey = "N/A";
    
    public Transform bulletOrigin;

    [SerializeField]
    private bool weaponOnlyFiresOnceWhenButtonIsPressed = true;

    private bool isFiring;

    [SerializeField]
    private WeaponType weaponType;

    [SerializeField]
    private PlayerCharacter user;

    [SerializeField]
    [Tooltip("The weapon fires these emissions on activation.")]
    private List<Emmission> emissions = new List<Emmission>();

    [SerializeField]
    [Tooltip("Played when the weapon is activated.")]
    private AudioClip onFireSound;

    [SerializeField]
    [Tooltip("Prevents onFireSounds from overlapping with each other when true.")]
    private bool dontInterruptOnFireSound;

    private AudioSource sound;

	// Use this for initialization
	private void Start ()
    {
        sound = GetComponent<AudioSource>();
        if (sound != null)
        {
            sound.clip = onFireSound;
            sound.playOnAwake = false;
        }
        user = GetComponentInParent<PlayerCharacter>();
        if(user != null)
        {
            Debug.Log(name + " has a parent user.");
        }
	}
	
	// Update is called once per frame
	private void Update () {
        AlignWeapon();
        GetInput();
        if (isFiring == true)
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
    
    public void GetInput()
    {
        if(weaponOnlyFiresOnceWhenButtonIsPressed)
        {
            isFiring = Input.GetButtonDown(InputKey);
        }
        else
        {
            isFiring = Input.GetButton(InputKey);
        }
    }

    public void Fire()
    {
        Debug.Log("Firing!");
        if(sound != null && onFireSound != null)
        {
            Debug.Log("sound and onFireSound are both valid.");
            if (dontInterruptOnFireSound)
            {
                if (!sound.isPlaying)
                {
                    sound.Play();
                }
            }
            else
            {
                sound.Play();
            }
        }
        foreach (Emmission emm in emissions)
        {
            emm.GetPhysics();
            Rigidbody2D bullet;
            bullet = Instantiate(emm.EmissionPhysics, bulletOrigin.position, bulletOrigin.rotation) as Rigidbody2D;
            if (user.IsFacingLeft() && !emm.bulletIgnoresWeaponRotation)
            {
                bullet.AddForce(-emm.Trajectory);
            }
            else
            {
                bullet.AddForce(emm.Trajectory);
            }
        }
    }

    public PlayerCharacter GetUser()
    {
        return user;
    }
}
