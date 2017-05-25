using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectileWeapon : MonoBehaviour, IWeapon
{
    public GameObject projectile;
    public int projectilesPerShot;
    public float launchSpeed;
    public float launchSpeedVariance;
    public float launchAngleVariance;
    public float shotInterval;
    public float shotDelay;

    protected float holdTime;
    protected float cooldown;
    protected Vector2 direction;
    protected Vector2 source;

    public void Equip(GameObject owner) {
        OnEquip(owner);
    }

    public void Unequip(GameObject owner)
    {
        OnUnequip(owner);
    }

    public void Ready(Vector2 target, Vector2 source, GameObject owner)
    {
        OnReady(target, source, owner);
    }

    public void Pull(GameObject owner)
    {
        if (holdTime == 0)
        {
            OnPull(owner);
        } else
        {
            OnHold(owner);
        }
    }

    public void Release(GameObject owner)
    {
        OnRelease(owner);
    }
    protected virtual void OnEquip(GameObject owner) { }

    protected virtual void OnUnequip(GameObject owner) { }

    protected virtual void OnReady(Vector2 target, Vector2 source, GameObject holder)
    {
        this.direction = (target - source).normalized;
        this.source = source;
        this.cooldown = Math.Max(0, cooldown - Time.deltaTime);
    }

    protected virtual void OnPull(GameObject holder)
    {
        holdTime = Time.deltaTime;
    }

    protected virtual void OnHold(GameObject holder)
    {
        holdTime += Time.deltaTime;
    }

    protected virtual void OnRelease(GameObject holder)
    {
        holdTime = 0;
    }

    protected void Shoot(GameObject holder)
    {
        for (int i = 0; i < projectilesPerShot; i++)
        {
            var attack = UnityEngine.Object.Instantiate(projectile);

            var rigidBody = attack.GetComponent<Rigidbody2D>();
            var body = attack.GetComponent<Collider2D>();
            var holderBody = holder.GetComponentsInParent<Collider2D>();
            Array.ForEach(holderBody, c => Physics2D.IgnoreCollision(c, body));

            attack.transform.position = source;
            attack.transform.up = direction;
            attack.transform.Rotate(0, 0, UnityEngine.Random.Range(-launchAngleVariance, launchAngleVariance));
            rigidBody.velocity = attack.transform.up * (launchSpeed + UnityEngine.Random.Range(-launchSpeedVariance, launchSpeedVariance));
        }
    }
}
