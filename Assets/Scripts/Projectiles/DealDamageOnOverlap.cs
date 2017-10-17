using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DealDamageOnOverlap : MonoBehaviour {
    public float damage;
    public bool once;
    public bool changeMaterial;
    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public Material newMaterial;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.collider.gameObject;
        DealDamage(other);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var other = collider.gameObject;
        DealDamage(other);
    }

    private void DealDamage(GameObject other)
    {
        var health = other.GetComponent<MonsterHealth>();
        if (health != null)
        {
            health.takeDamage(damage);
        }
        if (once)
        {
            enabled = false;
        }
        if (changeMaterial)
        {
            target.GetComponent<Renderer>().material = newMaterial;
        }
    }
}


[CustomEditor(typeof(DealDamageOnOverlap))]
public class DealDamageOnOverlapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DealDamageOnOverlap script = (DealDamageOnOverlap)target;
        if (script.changeMaterial)
        {
            script.target = (GameObject)EditorGUILayout.ObjectField("Target", script.target, typeof(GameObject), true);
            script.newMaterial = (Material)EditorGUILayout.ObjectField("New Material", script.newMaterial, typeof(Material), true);
        }
    }
}