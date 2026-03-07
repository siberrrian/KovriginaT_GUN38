using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallScript : MonoBehaviour
{
    private bool _ready;
    private Rigidbody _ball;

    [SerializeField]
    private Rigidbody _ballPrefab;
    [SerializeField]
    private float _startVelocity = 20f;
    [SerializeField]
    private Rigidbody Ball;
    [SerializeField]
    private float _lifetime;

    [SerializeField]
    private float _respawnDelay;


    void Update()
    {
        
        if (!_ready) return;
        //if (Input.GetKey(KeyCode.Space))

        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            StartCoroutine(Reloader());
            _ball.isKinematic = false;
            _ball.transform.parent = null;
            _ball.velocity = (new Vector3(-1, 0, 0)) * _startVelocity;
            Destroy(_ball.gameObject, _lifetime);
        }
    }

    private IEnumerator Reloader()
    {
        _ready = false;
        yield return new WaitForSeconds(_respawnDelay);
        Spawn();
    }

    private void Spawn()
    {
        _ball = Instantiate(_ballPrefab, transform.parent);
        _ball.transform.parent = transform;
        _ball.transform.position = transform.position;
        _ball.transform.position += new Vector3(0.0f, 1f, 0f);
        _ball.isKinematic = true;
        _ready = true;
    }

    private void Start()
    {
        Spawn();
    }
}
