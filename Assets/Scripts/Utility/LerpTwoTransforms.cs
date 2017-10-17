using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTwoTransforms : MonoBehaviour
{
    [SerializeField]
    private float lerp;
    private Transform t1;
    private Transform t2;
    [SerializeField]
    private Vector3 offset;

	void Update () {
        gameObject.transform.position = Vector3.Lerp(t1.position, t2.position, lerp) + offset;
	}

    public void Setup(Transform t1, Transform t2)
    {
        this.t1 = t1;
        this.t2 = t2;
    }
}
