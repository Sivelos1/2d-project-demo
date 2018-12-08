using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emmission : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The emission will spawn this GameObject when activated.")]
    public GameObject emission;

    [SerializeField]
    public Rigidbody2D EmissionPhysics;

    [SerializeField]
    [Tooltip("If this value is less than 1, the emission will have a X chance to spawn when activated. Used for things like random enemy drops.")]
    private float Probability = 1;

    [SerializeField]
    private bool pointTowardsCoordinateX, pointTowardsCoordinateY;

    [SerializeField]
    private float targetX, targetY;

    public Vector2 Trajectory
    {
        get
        {
            float x = (Mathf.Cos(Angle) * Speed);
            float y = (Mathf.Sin(Angle) * Speed);
            if (pointTowardsCoordinateX == true)
                x = targetX;
            if (pointTowardsCoordinateY == true)
                y = targetY;
            return new Vector2(x, y);
        }
        private set
        {

        }
    }

    [SerializeField]
    public bool bulletIgnoresWeaponRotation = false;

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
