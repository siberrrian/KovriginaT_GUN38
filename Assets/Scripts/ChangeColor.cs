using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private float _duration = 2f;
    [SerializeField] private MeshRenderer _meshRenderer;
    private float _startTime;
    private bool _isFadingOut;
    private void Start()
    {
        _startTime = Time.time;
    }
    private void Update()
    {
        ChangeTransperency();
    }


    private void ChangeTransperency()
    {
        float t = Mathf.Clamp01((Time.time - _startTime) / _duration);
        float alpha = _isFadingOut ? Mathf.Lerp(1, 0, t) : Mathf.Lerp(0, 1, t);
        _meshRenderer.material.color = new Color(_meshRenderer.material.color.r,
            _meshRenderer.material.color.g, _meshRenderer.material.color.b, alpha);
        if (t >= 1)
        {
            _isFadingOut = !_isFadingOut;
            _startTime = Time.time;
        }
    }

}
