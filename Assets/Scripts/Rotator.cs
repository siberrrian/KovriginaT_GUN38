using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotate = new Vector3(0f, 20f, 0f);

    private IEnumerator Start()
    {
        yield return new WaitForFixedUpdate();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        while (true)
        {
            rigidbody.transform.Rotate(_rotate * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
