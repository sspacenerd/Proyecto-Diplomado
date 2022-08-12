using UnityEngine;
//---- Items.cs
//---- Usando referencias de:
//---- youtube.com/watch?v=AoD_F1fSFFg&t=738s
[CreateAssetMenu(fileName = "New Item", menuName = "Create New Item")]
public class Item : ScriptableObject
{
    public enum ItemType { door, collectible, key, photo}
    public int id;
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
}
