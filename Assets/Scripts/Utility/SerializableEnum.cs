using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableEnum<T> where T : struct, IConvertible{

	public T Value
    {
        get { return SVar; }
        set { SVar = value; }
    }

    [SerializeField]
    private T SVar;
}
