using UnityEngine;
using System;

public class BonusTrigger : MonoBehaviour
{
    public Action OnPlayerEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (OnPlayerEntered != null)
            {
                OnPlayerEntered.Invoke();
            }
        }
    }
}
