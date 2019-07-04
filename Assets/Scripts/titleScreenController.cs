using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleScreenController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The elements, in order of top to bottom, that will be controlled and navigated by the player.")]
    private List<GameObject> menuItems = new List<GameObject>();

    

    [SerializeField]
    [Tooltip("The index of the currently selected menu item.")]
    private int menuSelectedIndex = 0;

    [SerializeField]
    [Tooltip("The object used as the cursor for the menu.")]
    private GameObject cursor;

    [SerializeField]
    [Tooltip("The X coordinate of the cursor.")]
    private float cursorX;

    [SerializeField]
    [Tooltip("The Y coordinates that cursor will take corresponding to each menu item.")]
    private List<float> menuItemCursorCoordinates = new List<float>();

    [SerializeField]
    [Tooltip("The amount of time, in frames, the game waits to allow the player to make another input. This is in place to prevent the player from being unable to control the menu due to taking in inputs too quickly.")]
    private float inputDelay = 30;

    [SerializeField]
    private float currentInputDelay = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentInputDelay >= -1)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    menuSelectedIndex -= 1;
                }
                else
                {
                    menuSelectedIndex += 1;
                }
                if (menuSelectedIndex < 0)
                {
                    menuSelectedIndex = menuItems.Count-1;
                }
                else if (menuSelectedIndex > menuItems.Count-1)
                {
                    menuSelectedIndex = 0;
                }
                currentInputDelay = inputDelay;
                return;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                switch (menuSelectedIndex)
                {
                    case 0:
                        print("Selected Play");
                        break;
                    case 1:
                        print("Selected Credits");
                        break;
                    case 2:
                        print("Selected Exit");
                        break;
                    default:
                        break;
                }
                currentInputDelay = inputDelay;
                return;
            }
        }
        else
        {
            currentInputDelay -= 1;
        }
        if (cursor != null)
        {
            cursor.transform.position = new Vector3(cursorX, menuItems[menuSelectedIndex].transform.position.y, 0);
        }
        
    }
}
