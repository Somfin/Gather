using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class AbstractProjectileWeapon : AbstractWeapon
{
    [Range (1, 100)]
    public float baseSpeed;
    [Range(0, 1)]
    public float shotInterval;
    [Range (0, 1)]
    public float shotDelay;

    public int projectilesPerShot;

    public bool alternatingOrientation;
    private bool orientation;

    public bool variableShots;
    [HideInInspector]
    public float speedVariance;
    [HideInInspector]
    public float angleVariance;


    protected List<GameObject> Shoot(GameObject holder)
    {
        var shot = new List<GameObject>();
        for (int i = 0; i < projectilesPerShot; i++)
        {
            var attack = SpawnAttack(holder);

            var speedChange = 0f;
            var angleChange = 0f;
            if (variableShots)
            {
                speedChange = UnityEngine.Random.Range(-speedVariance * 0.5f, speedVariance * 0.5f);
                angleChange = UnityEngine.Random.Range(-angleVariance * 0.5f, angleVariance * 0.5f);
            }

            attack.transform.position = source;
            Orientate(attack);
            attack.transform.Rotate(0, 0, angleChange);
            if (alternatingOrientation)
            {
                attack.transform.Rotate(0, orientation ? 180 : 0, 0);
                orientation = !orientation;
            }

            var body = attack.GetComponent<Rigidbody2D>();
            if (body != null)
            {
                body.velocity = attack.transform.up * (baseSpeed + speedChange);
            }

            shot.Add(attack);
        }
        return shot;
    }

    [CustomEditor(typeof(AbstractProjectileWeapon))]
    public class AbstractProjectileWeaponEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            AbstractProjectileWeapon script = (AbstractProjectileWeapon)target;
            if (script.variableShots)
            {
                script.speedVariance = EditorGUILayout.Slider("Speed Range", script.speedVariance, 0f, 30f);
                script.angleVariance = EditorGUILayout.Slider("Angle Range", script.angleVariance, 0f, 30f);
            }
        }
    }
}
