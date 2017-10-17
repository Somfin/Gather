using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReleaseToShoot : AbstractProjectileWeapon {
    [Range(0, 2)]
    public float minimumChargeThreshold;

    public bool reduceLifespan;

    [HideInInspector]
    public float additionalShotSpeed;

    [HideInInspector]
    public float additionalChargeRequired;

    private bool failed;

    protected override void OnPull(GameObject holder)
    {
        failed = false;
        base.OnPull(holder);
    }

    protected override void OnHold(GameObject holder)
    {
        if (reduceLifespan && !failed)
        {
            if ((holdTime - minimumChargeThreshold) > weaponAttack.GetComponent<Lifespan>().lifespan)
            {
                var shot = Shoot(holder);
                foreach (var attack in shot)
                {
                    var body = attack.GetComponent<Rigidbody2D>();
                    body.velocity = Vector2.zero;
                    var lifespan = attack.GetComponent<Lifespan>();
                    lifespan.lifespan -= (holdTime - minimumChargeThreshold);
                }
                cooldown = shotInterval;
                failed = true;
            }
        }
        base.OnHold(holder);
    }

    protected override void OnRelease(GameObject holder)
    {
        if (cooldown <= 0 && holdTime > minimumChargeThreshold && !failed)
        {
            var shot = Shoot(holder);
            if (additionalShotSpeed > 0)
            {
                var progress = Mathf.Min(1, (holdTime - minimumChargeThreshold) / additionalChargeRequired);
                var speedBonus = additionalShotSpeed * progress;
                foreach (var attack in shot)
                {
                    var body = attack.GetComponent<Rigidbody2D>();
                    var oldMagnitude = body.velocity.magnitude;
                    var newMagnitude = oldMagnitude + speedBonus;
                    var magnitudeMultiplier = newMagnitude / oldMagnitude;
                    body.velocity *= magnitudeMultiplier;
                }
            }

            if (reduceLifespan)
            {
                foreach (var attack in shot)
                {
                    var lifespan = attack.GetComponent<Lifespan>();
                    lifespan.lifespan -= (holdTime - minimumChargeThreshold);
                }
            }

            foreach (var attack in shot)
            {
                var holdReactives = attack.GetComponents<HoldReactive>();
                foreach (var reactive in holdReactives)
                {
                    reactive.ReactToHoldTime(holdTime);
                }
            }
            
            cooldown = shotInterval;
        }
        base.OnRelease(holder);
    }
}

[CustomEditor(typeof(ReleaseToShoot))]
public class ReleaseToShootEditor : AbstractProjectileWeapon.AbstractProjectileWeaponEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ReleaseToShoot script = (ReleaseToShoot)target;
        script.additionalShotSpeed = EditorGUILayout.Slider("Additional Shot Speed", script.additionalShotSpeed, 0f, 100f);
        if (script.additionalShotSpeed > 0)
        {
            script.additionalChargeRequired = EditorGUILayout.Slider("Speed Charge Time", script.additionalChargeRequired, 0f, 5f);
        }
    }
}