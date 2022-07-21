using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create New Item")]
public class Item : ScriptableObject
{
    public enum ItemType { door, collectible, etc}
    public int id;
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
}
