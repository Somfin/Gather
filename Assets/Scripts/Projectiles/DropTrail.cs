using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DropTrail : MonoBehaviour
{
    public bool onCollide;
    public bool onDestroy;
    public bool stillOnDrop;
    private Vector2 lastPoint;
    private Vector2 lastDirection;

    private void Start()
    {
        lastPoint = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        var currentPoint = lastPoint;
        lastPoint = gameObject.transform.position;
        lastDirection = (lastPoint - currentPoint);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onCollide)
        {
            lastPoint = collision.contacts.First().point;
            DropAll();
        }
    }

    public void LifespanTimeout()
    {
        if (onDestroy)
        {
            lastDirection = Vector3.zero;
            DropAll();
        }
    }

    private void OnDestroy()
    {
        if (onDestroy)
        {
            lastDirection = Vector3.zero;
            DropAll();
        }
    }

    private void DropAll()
    {
        var trail = GetComponentInChildren<TrailRenderer>();
        if (trail != null)
        {
            Drop(trail.gameObject);
            trail.autodestruct = true;
        }
        var particles = GetComponentInChildren<ParticleSystem>();
        if (particles != null)
        {
            Drop(particles.gameObject);
            particles.GetComponent<ParticleSystemAutoDestroy>().enabled = true;
        }
    }

    private void Drop(GameObject ob)
    {
        ob.transform.SetParent(null, true);
        if (!stillOnDrop)
        {
            RaycastHit2D hitPoint = Physics2D.Raycast(lastPoint, lastDirection, 1f);
            if (hitPoint.collider != null)
            {
                ob.transform.position = hitPoint.point;
            }
            else
            {
                ob.transform.position = lastPoint + lastDirection;
            }
        }
    }
}
