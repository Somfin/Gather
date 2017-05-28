using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public Type itemType;
    public RecipePart part;
    public List<RecipePart> recipe;
    
	public enum Type
    {
        WEAPON,
        EQUIPMENT,
        QUEST,
        PART
    }
    
    public enum RecipePart
    {
        NULL = 0,

        SPIKE_PART_1 = 101,
        SPIKE_WEAPON = 102,
        SPIKE_UPGRADE = 103,

        KNIFE_PART_1 = 201,
        KNIFE_PART_2 = 202,
        KNIFE_WEAPON = 203,
        KNIFE_UPGRADE = 204
    }
}