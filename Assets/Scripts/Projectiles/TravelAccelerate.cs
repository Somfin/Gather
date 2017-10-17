using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelAccelerate : MonoBehaviour
{
    public float force;

    private Rigidbody2D body;

    private void Start()
    {
        this.body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.AddForce((Vector2)body.transform.up * force * Time.deltaTime, ForceMode2D.Force);
    }
}