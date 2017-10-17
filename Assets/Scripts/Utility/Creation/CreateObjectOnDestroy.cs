using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateObjectOnDestroy : AbstractCreateObject
{
    private void OnDestroy()
    {
        CreateObjects(gameObject.transform.position);
    }
}
