using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emmission : MonoBehaviour {

    [SerializeField]
    public GameObject emission;

    [SerializeField]
    public Rigidbody2D EmissionPhysics;

    [SerializeField]
    private float Probability = 1;

    [SerializeField]
    private bool PointTowardsCoordinateX, PointTowardsCoordinateY;

    [SerializeField]
    private float TargetX, TargetY;

    public Vector2 Trajectory
    {
        get
        {
            float x = (Mathf.Cos(Angle) * Speed);
            float y = (Mathf.Sin(Angle) * Speed);
            if (PointTowardsCoordinateX == true)
                x = TargetX;
            if (PointTowardsCoordinateY == true)
                y = TargetY;
            return new Vector2(x, y);
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
        EmissionPhysics = emission.GetComponent<Rigidbody2D>();
    }

    public bool GetChanceToFire()
    {
        float rng = Random.Range(0f, 1f);
        Debug.Log((rng*100)+"% to a probability of "+(Probability*100)+"%.");
        if(rng <= Probability)
        {
            Debug.Log("Roll succeeded!");
            return true;
        }
        else
        {
            Debug.Log("Roll failed.");
            return false;
        }
    }
}
