using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVEnemy : MonoBehaviour
{
    public float viewRadius = 10f;

    [Range(0, 360)]
    public float viewAngle = 90f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    private Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        FindVisibleTargets();
    }

    void FindVisibleTargets()
    {
        float distanceToTarget = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToTarget <= viewAngle)
        {
            Vector3 dirToTarget = (playerTransform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            { 
                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                {
                    Debug.Log("Игрок замечен");
                }
            }

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);

    }

    public Vector3 DirFromAngle(float angleInDegress, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegress += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegress*Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegress*Mathf.Deg2Rad));

    }
}
