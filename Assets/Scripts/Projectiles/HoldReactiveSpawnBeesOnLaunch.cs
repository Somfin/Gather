using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldReactiveSpawnBeesOnLaunch : MonoBehaviour, HoldReactive, InheritCollisionIgnore
{
    public GameObject bee;
    public int minimumCount;
    public int maximumCount;
    public float chargeTime;
    public float beeSpeed;

    private GameObject ignore;

    public void IgnoreCollisions(GameObject ignore)
    {
        this.ignore = ignore;
    }

    public void ReactToHoldTime(float holdTime)
    {
        var proportion = Mathf.Min(1, holdTime / chargeTime);
        var difference = maximumCount - minimumCount;
        var count = minimumCount + Mathf.Floor(maximumCount * proportion);
        for (int i = 0; i < count; i++)
        {
            var attack = Instantiate(bee);
            attack.transform.position = gameObject.transform.position;
            attack.transform.up = UnityEngine.Random.insideUnitCircle.normalized;
            attack.GetComponent<Rigidbody2D>().velocity = attack.transform.up * beeSpeed;
            var holderBody = ignore.GetComponentsInParent<Collider2D>();
            Array.ForEach(holderBody, c => Physics2D.IgnoreCollision(c, attack.GetComponent<Collider2D>()));
            attack.GetComponent<TravelBee>().SetTarget(gameObject);
        }
    }
}
