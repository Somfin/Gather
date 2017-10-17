using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfOnCollide : MonoBehaviour {
    private bool active;

    private void Update()
    {
        if (this.isActiveAndEnabled)
        {
            active = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyIfActive();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        DestroyIfActive();
    }

    private void DestroyIfActive()
    {
        if (active)
        {
            Destroy(gameObject);
        }
    }
}
