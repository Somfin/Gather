using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialOnDelay : MonoBehaviour
{
    public Material material;
    public GameObject target;
    public float delayTime;

    private void Start()
    {
        StartCoroutine(WaitAndChange(delayTime));
    }

    private IEnumerator WaitAndChange(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        target.GetComponent<Renderer>().material = material;
    }
}
