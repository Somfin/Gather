using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrailOnDestroy : MonoBehaviour {
    private void OnDestroy()
    {
        var trail = GetComponentInChildren<TrailRenderer>();
        if (trail != null)
        {
            trail.transform.parent = null;
            trail.autodestruct = true;
        }
    }
}
