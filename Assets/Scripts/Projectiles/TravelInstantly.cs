using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelInstantly : MonoBehaviour {
    public float maxDistance;
    public LayerMask hittable;

    private void FixedUpdate()
    {
        var line = GetComponentInChildren<LineRenderer>();
        line.SetPosition(0, gameObject.transform.position);
        var hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.up, maxDistance, hittable);
        if (hit.collider != null)
        {
            gameObject.transform.position = hit.point;
        } else
        {
            gameObject.transform.position += gameObject.transform.up * maxDistance;
        }
        line.SetPosition(1, gameObject.transform.position);
        line.enabled = true;
        line.gameObject.transform.parent = null;
        this.enabled = false;
    }
}