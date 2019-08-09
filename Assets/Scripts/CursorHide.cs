using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHide : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
        Cursor.visible = false;
    }
}
