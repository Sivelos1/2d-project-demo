using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionValue
{
    Right,
    Left
}

public class Direction : MonoBehaviour {

    private Transform objectTransform;

    public DirectionValue DirectionFacing;

    [SerializeField]
    private bool spriteIgnoresDirection;

    void Start()
    {
        objectTransform = gameObject.transform;
    }
	
	// Update is called once per frame
	void Update () {
        objectTransform = gameObject.transform;
        if (spriteIgnoresDirection == false)
        {
            switch (DirectionFacing)
            {
                case DirectionValue.Left:
                    objectTransform.rotation = new Quaternion(0, 180, 0, 0);
                    break;
                case DirectionValue.Right:
                    objectTransform.rotation = new Quaternion(0, 0, 0, 0);
                    break;
                default:
                    objectTransform.rotation = new Quaternion(0, 0, 0, 0);
                    break;
            }
        }
        else
        {
            objectTransform.rotation = new Quaternion(0, 0, 0, 0);
        }
	}

    public void Turn180Degrees()
    {
        if(DirectionFacing == DirectionValue.Left)
            DirectionFacing = DirectionValue.Right;

        if(DirectionFacing == DirectionValue.Right)
            DirectionFacing = DirectionValue.Left;
    }
    public void TurnLeft()
    {
        DirectionFacing = DirectionValue.Left;
    }
    public void TurnRight()
    {
        DirectionFacing = DirectionValue.Right;
    }
}
