using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CreateObjectOnDelay : AbstractCreateObject
{
    public float delay;

    private void Start()
    {
        StartCoroutine(CreateOnDelay());
    }

    private IEnumerator CreateOnDelay()
    {
        yield return new WaitForSeconds(delay);
        CreateObjects(gameObject.transform.position);
    }
}
