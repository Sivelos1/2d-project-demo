using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emmission : MonoBehaviour {

    [SerializeField]
    public Bullet Bullet;

    [SerializeField]
    public Rigidbody2D EmissionPhysics;

    public Vector2 Trajectory
    {
        get
        {
            return new Vector2((Mathf.Cos(Angle) * Speed), (Mathf.Sin(Angle) * Speed));
        }
        private set
        {

        }
    }

    [SerializeField]
    public bool IgnoreWeaponRotation = false;

    [SerializeField]
    private float Angle;

    [SerializeField]
    private float Speed;

    public void GetPhysics()
    {
        Debug.Log("CALLED START FOR EMISSION");
        EmissionPhysics = Bullet.GetComponent<Rigidbody2D>();
    }
}
