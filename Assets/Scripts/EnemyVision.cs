using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;

    public LayerMask targetPlayer;
    public LayerMask obstacleMask;

    public GameObject player;

    public bool detected;
    public Vector3 lastSeenPos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void FixedUpdate()
    {
        if (player != null)
        {

            Vector3 playerTarget = (player.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
                if (distanceToTarget <= viewRadius)
                {
                    if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, obstacleMask) == false)
                    {

                        detected = true;
                        lastSeenPos = player.transform.position;
                    }
                    else
                    {
                        detected = false;
                    }
                }
                else detected = false;
            }
            else detected = false;

        }
        else detected = false;

    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // Draw the view radius
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireArc(transform.position, Vector3.up, transform.forward, 360, viewRadius);

        // Draw the view angle
        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);
        UnityEditor.Handles.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        UnityEditor.Handles.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);

        // Draw a line from the object to the last seen position
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawLine(transform.position, lastSeenPos);
    }
#endif

    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
