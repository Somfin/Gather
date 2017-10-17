using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateComponentsOnDelay : MonoBehaviour {
    public List<MonoBehaviour> components;
    public List<Collider2D> colliders;
    public List<GameObject> gameObjects;
    public float delayTime;

    private void Start()
    {
        StartCoroutine(WaitAndActivate(delayTime));
    }

    private IEnumerator WaitAndActivate(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        foreach (var component in components)
        {
            component.enabled = true;
        }
        foreach (var component in colliders)
        {
            component.enabled = true;
        }
        foreach (var gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
    }
}
