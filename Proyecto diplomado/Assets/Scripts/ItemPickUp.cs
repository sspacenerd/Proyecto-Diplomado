using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//---- ItemPickUp.cs
//---- Usando referencias de:
//---- youtube.com/watch?v=AoD_F1fSFFg&t=738s
public class ItemPickUp : MonoBehaviour
{
    public Item item;
    
    public void PickUp()
    {
        InventorySystem.instance.Add(item);
        Destroy(gameObject);
    }
}
