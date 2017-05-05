using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullToShoot : AbstractProjectileWeapon {
    protected override void OnPull(GameObject holder)
    {
        base.OnPull(holder);
        if (cooldown <= 0)
        {
            Shoot(holder);
            cooldown = shotInterval;
        }
    }
}
