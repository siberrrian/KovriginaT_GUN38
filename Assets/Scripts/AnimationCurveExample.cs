using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurveExample : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private AnimationCurve _lerpCurve;
    [SerializeField] private Vector3 _lerpOffset;
    [SerializeField] private float _lerpTime = 3;
    private float _timer;
    private void Update() {
        _timer += Time.deltaTime;
        if (_timer > _lerpTime)
        {
            _timer = _lerpTime;
        }

        float _lerpRation = _timer / _lerpTime;
        Vector3 positionOffset = _lerpCurve.Evaluate(_lerpRation) * _lerpOffset;
        transform.position = Vector3.Lerp(_pointA.position, _pointB.position, _lerpRation) + positionOffset;
    }
}