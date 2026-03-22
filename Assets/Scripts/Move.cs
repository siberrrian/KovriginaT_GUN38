using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _interval;

    private Vector3 _targetPosition;
    private bool _isMoving;
    private void Start()
    {
        _targetPosition = _pointA.position;
    }

    private void Update()
    {
        if (!_isMoving)
        {
            if (Time.time >= _interval)
            {
                _isMoving = true;
                StartCoroutine(MoveToTarget());
                _interval += 4f;
            }

        }
    }

    private IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, _targetPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime);
            yield return null;

        }


        _isMoving = false;
        if(_targetPosition == _pointA.position)
        {
            _targetPosition = _pointB.position;
        }

        else
        {
            _targetPosition = _pointA.position;
        }
    }  
}