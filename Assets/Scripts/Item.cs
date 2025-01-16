
using UnityEngine;


[CreateAssetMenu(fileName = "New item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public int weight;
    public Sprite icon;
    public ItemType itemType;
    

    public enum ItemType
    {
        Bandage,
        Ammo,
        Weapon
    }
    
}
