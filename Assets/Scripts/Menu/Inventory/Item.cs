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
        KNIFE_UPGRADE = 204,

        BOW_PART_1 = 301,
        BOW_PART_2 = 302,
        BOW_WEAPON = 303,
        BOW_UPGRADE = 304,

        SHIELD_PART_1 = 401,
        SHIELD_PART_2 = 402,
        SHEILD_WEAPON = 403,
        SHIELD_UPGRADE = 404,

        EGG_PART_1 = 501,
        EGG_PART_2 = 502,
        EGG_PART_3 = 503,
        EGG_WEAPON = 504,
        EGG_UPGRADE = 505,

        FLAME_PART_1 = 601,
        FLAME_PART_2 = 602,
        FLAME_PART_3 = 603,
        FLAME_WEAPON = 604,
        FLAME_UPGRADE = 605,
        
        LIGHTNING_PART_1 = 701,
        LIGHTNING_PART_2 = 702,
        LIGHTNING_PART_3 = 703,
        LIGHTNING_WEAPON = 704,
        LIGHTNING_UPGRADE = 705,

        BEES_PART_1 = 801,
        BEES_PART_2 = 802,
        BEES_PART_3 = 803,
        BEES_WEAPON = 804,
        BEES_UPGRADE = 805,

        CANNON_PART_1 = 901,
        CANNON_PART_2 = 902,
        CANNON_PART_3 = 903,
        CANNON_WEAPON = 904,
        CANNON_UPGRADE = 905,

        WRIT_PART_1 = 1001,
        WRIT_PART_2 = 1002
    }
}