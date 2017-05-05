using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfOnCollide : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
