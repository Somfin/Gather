using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelLightning : MonoBehaviour
{
    public float maxDistance;
    public LayerMask hittable;

    public float jumpDistance;
    public float jumpDistanceVariance;
    public float jumpAngle;

    private Vector3 target;

    private void Start()
    {
        target = gameObject.transform.position + (gameObject.transform.up * maxDistance);
    }

    private void FixedUpdate()
    {
        var line = GetComponentInChildren<LineRenderer>();

        var distance = 0f;
        var points = new List<Vector3>();
        points.Add(gameObject.transform.position);

        while (distance < maxDistance)
        {
            distance += Jump(points);
        }

        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
        line.enabled = true;
        line.gameObject.transform.parent = null;
        this.enabled = false;
    }

    private float Jump(List<Vector3> points)
    {
        var targetRotation = Vector3.SignedAngle(gameObject.transform.up, target - gameObject.transform.position, Vector3.forward);
        gameObject.transform.Rotate(0, 0, targetRotation);
        gameObject.transform.Rotate(0, 0, Random.Range(-jumpAngle, jumpAngle));

        var distance = 0f;
        var jump = jumpDistance + (jumpDistanceVariance - Random.Range(0, jumpDistanceVariance));
        var hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.up, jump, hittable);
        if (hit.collider != null)
        {
            gameObject.transform.position = hit.point;
            distance = maxDistance;
        }
        else
        {
            gameObject.transform.position = gameObject.transform.position + (gameObject.transform.up * jump);
            distance = jump;
        }
        points.Add(gameObject.transform.position);
        return distance;
    }
}