using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour {
    public float lifespan;
    public bool singleFrame;
	// Update is called once per frame
	void Update () {
        lifespan -= Time.deltaTime;
		if (lifespan < 0 && !singleFrame)
        {
            var trail = gameObject.GetComponent<DropTrail>();
            if (trail != null) {
                trail.LifespanTimeout();
            }
            Destroy(gameObject);
        }
	}

    private void FixedUpdate()
    {
        if (singleFrame)
        {
            singleFrame = !singleFrame;
            lifespan = 0;
        }
    }
}
