using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    
    public void PickUp()
    {
        InventorySystem.instance.Add(item);
        Destroy(gameObject);
    }
}
