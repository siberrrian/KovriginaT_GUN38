using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float rayLength = 5f;
    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private Color hitColor = Color.green;
    [SerializeField]
    private Color missColor = Color.red;
    [SerializeField]
    private float speed = 1.2f;
    void Update()
    { 
        Vector2 direction = transform.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, targetLayer);

        Color raycolor = missColor;
        if (hit.collider != null)
        {
            raycolor = hitColor;
        }

        Debug.DrawRay(transform.position, direction * rayLength, raycolor);



        if (hit.collider == null)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        } else
        {
            transform.Rotate(0, 0, 180f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
        }
    }

    
}
