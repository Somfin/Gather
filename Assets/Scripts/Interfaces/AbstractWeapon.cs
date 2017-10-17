using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon: MonoBehaviour, IWeapon
{
    public GameObject weaponAttack;
    public float spawnXOffset;
    public float spawnYOffset;
    protected float holdTime;
    protected float cooldown;
    protected Vector2 direction;
    protected Vector2 source;

    public void Equip(GameObject owner)
    {
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
        }
        else
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
        Debug.DrawLine(source, target);
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

    protected GameObject SpawnAttack(GameObject holder)
    {
        var attack = GameObject.Instantiate(weaponAttack);
        var rigidBody = attack.GetComponent<Rigidbody2D>();
        var body = attack.GetComponent<Collider2D>();
        var holderBody = holder.GetComponentsInParent<Collider2D>();
        Array.ForEach(holderBody, c => Physics2D.IgnoreCollision(c, body));
        var generator = attack.GetComponent<InheritCollisionIgnore>();
        if (generator != null) {
            generator.IgnoreCollisions(holder);
        };
        return attack;
    }

    protected void Orientate(GameObject attack)
    {
        attack.transform.up = direction;
        attack.transform.position = source
            + ((Vector2)attack.transform.up * spawnXOffset)
            + ((Vector2)attack.transform.right * (direction.x > 0 ? -spawnYOffset : spawnYOffset));
    }
}