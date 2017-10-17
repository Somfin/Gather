using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class CreateObjectOnCollide : AbstractCreateObject
{
    public bool once;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CreateObjects(collision.contacts.First().point);
        if (once)
        {
            doNotCreate = true;
        }
    }
}
