using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    public void ItemInteract()
    {
    }
    public void AddItem(Item newItem)
    {
        item = newItem;

    }
    public void DestroyItem()
    {
        InventorySystem.instance.Remove(item);
        Destroy(gameObject);
    }
}
