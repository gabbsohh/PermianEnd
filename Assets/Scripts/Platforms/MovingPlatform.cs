using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform platformTrigger;
    public Transform startPoint;
    public Transform endPoint;
    public Transform triggerStartPoint;
    public Transform triggerEndPoint;

    //public Rigidbody2D rb;
    public BoxCollider2D bc;

    public float speed = 1.5f;

    int direction = 1;

    private void Update()
    {
        Vector2 target = currentMovementTarget();
        Vector2 triggerTarget = currentTriggerTarget();

        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);
        platformTrigger.position = Vector2.Lerp(platformTrigger.position, triggerTarget, speed * Time.deltaTime);

        float distance = (target - (Vector2)platform.position).magnitude;

        if(distance <= 0.1f)
        {
            direction *= -1;
        }

    }

    Vector2 currentMovementTarget()
    {
        if(direction == 1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }

    Vector2 currentTriggerTarget()
    {
        if(direction == 1)
        {
            return triggerStartPoint.position;
        }
        else
        {
            return triggerEndPoint.position;
        }
    }

    private void OnDrawGizmos()
    {
        // Debug Visualization for Moving Platform Lines
        if(platform!=null && startPoint!=null && endPoint!=null)
        {
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }
}