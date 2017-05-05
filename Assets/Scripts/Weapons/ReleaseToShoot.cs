using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseToShoot : AbstractProjectileWeapon {
    protected override void OnRelease(GameObject holder)
    {
        if (cooldown <= 0)
        {
            Shoot(holder);
            cooldown = shotInterval;
        }
        base.OnRelease(holder);
    }
}
