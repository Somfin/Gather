using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DealDamageOnCollide))]
public class LoseDamageOnCollide : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<DealDamageOnCollide>().damage = 0;
    }
}