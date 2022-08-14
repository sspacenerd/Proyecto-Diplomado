using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//---- InventoryItemController.cs
//---- Usando referencias de:
//---- youtube.com/watch?v=AoD_F1fSFFg&t=738s
public class InventoryItemController : MonoBehaviour
{
    Item item;
    public void ItemInteract()
    {
        InventorySystem.instance.Remove(item);

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
    public void SwitchItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.bottle:
                {
                    GameManager.gameManagerInstance.MyEffect();
                    DestroyItem();
                    break;
                }
        }
    }
}
