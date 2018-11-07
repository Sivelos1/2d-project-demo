using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transfer : MonoBehaviour {

    [SerializeField]
    private string TargetScene;

    [SerializeField]
    private bool RequiresInput;

    [SerializeField]
    private bool RoundTrip;

    [SerializeField]
    private bool TransfersToPointInSameLevel;

    [SerializeField]
    private bool UseTargetCoordinates;

    [SerializeField]
    private float TargetX;
    [SerializeField]
    private float TargetY;

    [SerializeField]
    private GameObject Target;

    private PlayerCharacter user;

    [SerializeField]
    private bool UsedTransfer = false;

    [SerializeField]
    bool Locked = false;
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            user = collision.GetComponentInParent<PlayerCharacter>();
            if (user.GetIsDeadValue() == true)
                return;

            if(Locked == true)
            {
                Debug.Log("Locked.");
                return;
            }

            if (RoundTrip == false && UsedTransfer == true)
            {
                Debug.Log("This transfer cannot be used more than once.");
                return;
            }
            if (RequiresInput == true)
            {
                if (Input.GetButtonDown("Activate"))
                {
                    ActivateTransfer();
                    Debug.Log("Tu du du~ thanks for using the transfer.");
                }
            }
            else
            {
                ActivateTransfer();
                Debug.Log("Tu du du~ thanks for using the transfer.");
            }
            

        }
    }

    private void ActivateTransfer()
    {
        if (TransfersToPointInSameLevel == false)
        {
            if (TargetScene == null || TargetScene == "")
            {
                Debug.Log("This transfer has no target.");
                return;
            }
            Debug.Log("The player activated the door.");
            user.transform.position = new Vector3(TargetX, TargetY);
            if (RoundTrip == false)
            {
                UsedTransfer = true;
            }

            if (UseTargetCoordinates == true)
            {
                Debug.Log("The player activated the door.");
                user.transform.position = new Vector3(TargetX, TargetY);
                if (RoundTrip == false)
                {
                    UsedTransfer = true;
                }
            }
        }
        else
        {

            if (UseTargetCoordinates == true)
            {

                Debug.Log("The player activated the door.");
                user.transform.position = new Vector3(TargetX, TargetY);
                if (RoundTrip == false)
                {
                    UsedTransfer = true;
                }
            }
            else if (Target != null)
            {
                Debug.Log("The player activated the door.");
                user.transform.position = Target.transform.position;
                if (RoundTrip == false)
                {
                    UsedTransfer = true;
                }
            }
            else
            {
                Debug.Log("The transfer has no target.");
            }
            

            
        }
    }
}
