using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfOnDetectObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
