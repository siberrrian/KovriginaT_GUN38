using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Tilemap _map;
    private Camera _camera;

    void Start()
    {
        _map = GetComponent<Tilemap>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            _player.transform.position = new Vector3(clickWorldPosition.x, clickWorldPosition.y, 0);

        }
    }
}
