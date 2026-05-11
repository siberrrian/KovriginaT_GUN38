using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class Trap2Move : MonoBehaviour
{
    [SerializeField] private float distance = 5f;
    [SerializeField] private float speed = 2f;
    private Vector3 _startPos;

    void Start() => _startPos = transform.position;

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * distance;
        transform.position = _startPos + new Vector3(0, 0, offset);
    }
}
