using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallScript : MonoBehaviour
{
    public float throwForce = 20f;
    [SerializeField]
    private Rigidbody Ball;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            ThrowBall();
        }
    }

    void ThrowBall()
    {
        Ball.isKinematic = false;
        Vector3 f = new Vector3(-1, 0, 0);
        Ball.AddForce(f * throwForce, ForceMode.Impulse);
    }
}
