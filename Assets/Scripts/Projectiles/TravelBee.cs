using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelBee : MonoBehaviour {
    public float force;
    public float chaos;

    private Rigidbody2D body;
    private GameObject target;

    private void Start()
    {
        this.body = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    private void FixedUpdate()
    {
        gameObject.transform.up = target.transform.position - gameObject.transform.position;
        body.AddForce(((Vector2)body.transform.up + Random.insideUnitCircle * chaos ) * force * Time.deltaTime, ForceMode2D.Force);
    }
}