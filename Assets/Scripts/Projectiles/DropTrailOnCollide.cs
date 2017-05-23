using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DropTrailOnCollide : MonoBehaviour
{
    public LayerMask hittable;
    private Vector2 lastPoint;
    private Vector2 lastDirection;
    private Rigidbody2D body;
    private bool moved;

    private void Start()
    {
        body = GetComponentInParent<Rigidbody2D>();
        lastPoint = body.position;
    }

    private void FixedUpdate()
    {
        var currentPoint = lastPoint;
        lastPoint = body.position;
        lastDirection = (lastPoint - currentPoint);
        if (lastDirection.magnitude > 0)
        {
            moved = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var trail = GetComponentInChildren<TrailRenderer>();
        if (trail != null)
        {
            if (!moved)
            {
                Destroy(trail.gameObject);
            }
            trail.transform.parent = null;
            RaycastHit2D hitPoint = Physics2D.Raycast(lastPoint, lastDirection, 1f, hittable);
            if (hitPoint.collider != null)
            {
                trail.transform.position = hitPoint.point;
            } else
            {
                trail.transform.position = lastPoint + lastDirection;
            }
            trail.autodestruct = true;
        }
    }
}
