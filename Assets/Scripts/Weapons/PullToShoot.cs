using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullToShoot : AbstractProjectileWeapon {
    public bool allowQueue;
    public bool attachToHolder;
    private bool queued;

    protected override void OnReady(Vector2 target, Vector2 source, GameObject holder)
    {
        base.OnReady(target, source, holder);
        if (cooldown <= 0 && allowQueue && queued)
        {
            PullShoot(holder);
            queued = false;
        }
    }

    protected override void OnPull(GameObject holder)
    {
        base.OnPull(holder);
        if (allowQueue)
        {
            queued = true;
        } else if (cooldown <= 0)
        {
            PullShoot(holder);
        }
    }

    private void PullShoot(GameObject holder)
    {
        holder.GetComponent<AbstractCombatHandler>().StartCoroutine(FireOnDelay(holder));
        cooldown = shotInterval;
    }

    private IEnumerator FireOnDelay(GameObject holder)
    {
        yield return new WaitForSeconds(shotDelay);
        var shot = Shoot(holder);
        if (attachToHolder)
        {
            foreach(var s in shot)
            {
                s.transform.SetParent(holder.transform, true);
            }
        }
    }
}
