using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;
    public List<Item> items = new List<Item>();
    public Transform itemContent;
    public GameObject itemReferencePrefab, inventoryReference;
    public InventoryItemController[] inventoryItems;
    GameObject player;
    public bool isOpen;
    // Start is called before the first frame update 
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isOpen)
            {
                ListItems();
                SetInventoryItems();
                inventoryReference.transform.DOLocalMoveX(-709f, 1);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                player.GetComponent<PlayerController>().mouseSensitivity = 0;
                player.GetComponentInChildren<PickUp>().rayDistance = 0;
                isOpen = true;
            }
            else
            {
                inventoryReference.transform.DOLocalMoveX(-1211, 1);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                player.GetComponent<PlayerController>().mouseSensitivity = 3;
                player.GetComponentInChildren<PickUp>().rayDistance = 3;
                isOpen = false;
            }
        }
    }
    public void Add(Item item)
    {
        items.Add(item);
    }
    public void Remove(Item item)
    {
        items.Remove(item);
    }
    public void ListItems()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
        foreach(var item in items)
        {
            GameObject obj = Instantiate(itemReferencePrefab, itemContent);
            var itemIcon = obj.transform.Find("Icon").GetComponent<Image>();
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();

           // itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }
    public void SetInventoryItems()
    {
        inventoryItems = itemContent.GetComponentsInChildren<InventoryItemController>();

        for(int i =0; i< items.Count; i++)
        {
            inventoryItems[i].AddItem(items[i]);
        }

    }
    public void InstantiateGameObject()
    {
        inventoryItems = itemContent.GetComponentsInChildren<InventoryItemController>();
    }
} 
