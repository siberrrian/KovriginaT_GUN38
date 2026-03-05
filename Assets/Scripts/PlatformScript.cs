using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformScript : MonoBehaviour
{

    [SerializeField]
    private Transform Cube;

    // Start is called before the first frame update
    void Start()
    {
        PlatformMove();

        
    }

    // Update is called once per frame
    void Update()
    {if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            
        }
        
    }

    void PlatformMove()
    {
        while (!Pointer.current.press.wasPressedThisFrame)
        {

        }
    }
}
