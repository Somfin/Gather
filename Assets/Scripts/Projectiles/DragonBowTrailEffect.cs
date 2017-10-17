using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBowTrailEffect : MonoBehaviour {
    public float effectSpeed;
    private LineRenderer line;
    private float startStartWidth;
    private float startEndWidth;
    private float currentScale;

	// Use this for initialization
	void Start ()
    {
        line = GetComponent<LineRenderer>();
        startStartWidth = line.startWidth;
        startEndWidth = line.endWidth;
        currentScale = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentScale = Mathf.Lerp(currentScale, 0, Time.deltaTime * effectSpeed);
        line.widthMultiplier = currentScale;

        line.SetPosition(0, Vector3.Lerp(line.GetPosition(0), line.GetPosition(1), Time.deltaTime * effectSpeed / 2));
    }
}
