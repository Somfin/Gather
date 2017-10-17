using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldToHold : AbstractWeapon {
    private GameObject currentHeld;

    protected override void OnPull(GameObject holder)
    {
        base.OnPull(holder);
        currentHeld = SpawnAttack(holder);
        Orientate(currentHeld);
    }

    protected override void OnHold(GameObject holder)
    {
        base.OnHold(holder);
        base.Orientate(currentHeld);
    }

    protected override void OnRelease(GameObject holder)
    {
        base.OnRelease(holder);
        Destroy(currentHeld);
    }
}
