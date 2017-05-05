using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMouseMovement : MonoBehaviour {
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane hPlane = new Plane(Vector3.back, Vector3.zero);
        float distance = 0;
        if (hPlane.Raycast(ray, out distance))
        {
            // get the hit point:
            gameObject.transform.position = ray.GetPoint(distance);
        }
	}
}
