using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTwoTransforms : MonoBehaviour {
    public Transform t1;
    public Transform t2;
    public float lerp;

	void Update () {
        gameObject.transform.position = Vector3.Lerp(t1.position, t2.position, lerp);
	}
}
