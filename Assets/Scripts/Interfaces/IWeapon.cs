using UnityEngine;

public interface IWeapon 
{
    void Equip(GameObject owner);
    void Ready(Vector2 mousePosition, Vector2 sourcePosition, GameObject owner);
    void Pull(GameObject owner);
    void Release(GameObject owner);
    void Unequip(GameObject owner);
}
