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
        KNIFE_PART_1,
        KNIFE_PART_2,
        KNIFE_WEAPON,
        KNIFE_UPGRADE
    }

    public class SRecipePart : SerializableEnum<RecipePart>{}
}