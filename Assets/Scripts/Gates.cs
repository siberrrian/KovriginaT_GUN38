using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour
{
    private int _score = 0;
    private void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null )
        {
            _score++;
            Debug.Log($"Goal! Score: {_score}");
            Destroy(other.gameObject);
        }
        
    }
}
