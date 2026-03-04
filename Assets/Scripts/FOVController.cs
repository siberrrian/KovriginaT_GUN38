using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVController : MonoBehaviour
{
    public Camera mainCamera;
    public float fovSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            // mainCamera.fieldOfView -= fovSpeed * Time
        }
    }
}
