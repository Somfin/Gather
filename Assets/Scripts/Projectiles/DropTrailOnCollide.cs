using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DropTrailOnCollide : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var trail = GetComponentInChildren<TrailRenderer>();
        if (trail != null)
        {
            trail.transform.parent = null;
            trail.transform.position = Vector3.Lerp(collision.contacts.First().point, trail.transform.position, 0.5f);
            trail.autodestruct = true;
        }
    }
}
