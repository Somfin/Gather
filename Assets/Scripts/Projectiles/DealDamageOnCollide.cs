using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageOnCollide : MonoBehaviour {
    public float damage;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.collider.gameObject;
        var health = other.GetComponent<MonsterHealth>();
        if (health != null)
        {
            health.takeDamage(damage);
        }
    }
}
