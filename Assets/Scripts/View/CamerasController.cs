using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private Transform _minMapCamera;

    [SerializeField] private Transform _player;

    void Update()
    {
        _mainCamera.transform.position = _player.transform.position;
        _minMapCamera.transform.position = _player.transform.position;

    }
}
