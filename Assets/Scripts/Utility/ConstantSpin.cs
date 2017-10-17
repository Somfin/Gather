using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpin : MonoBehaviour {
    public float rotateX;
    public float rotateY;
    public float rotateZ;
	// Update is called once per frame
	void Update () {
        gameObject.transform.Rotate(rotateX * Time.deltaTime, rotateY * Time.deltaTime, rotateZ * Time.deltaTime);
	}
}
