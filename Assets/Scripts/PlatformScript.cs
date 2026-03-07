using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformScript : MonoBehaviour
{

    [SerializeField]
    private Vector3 _start = new Vector3(10f, 0f, -23.98f);
    [SerializeField]
    private Vector3 _end = new Vector3(-10f, 0f, -23.98f);

    [SerializeField]
    private float _moveTime = 1f;
    [SerializeField]
    private float _delayTime = 2f;
    private Vector3[] _positions;


    private IEnumerator Start()
    {
        _positions = new Vector3[] { _start, _end };
        if (_positions.Length < 2) yield break;
        int prev = 0, curr = 1;
        var time = 0f;
        var transform = this.transform;
        while (true)
        {
            transform.position = Vector3.Lerp(_positions[prev], _positions[curr], time / _moveTime);
            time += Time.deltaTime;
            if (time >= _moveTime)
            {
                time = 0f;
                prev = curr;
                curr = (curr + 1) % _positions.Length;
                yield return new WaitForSeconds(_delayTime);
            }

            yield return null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_start, 0.5f);
        Gizmos.DrawWireSphere(_end, 0.5f);
        Gizmos.DrawLine(_start, _end);
    }
}
