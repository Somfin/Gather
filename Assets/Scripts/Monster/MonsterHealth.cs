using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour {
    public float maxHealth;
    private float currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }
	}

    internal void takeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
