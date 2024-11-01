using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public GameObject Inventory;
    private const int MaxItems = 1;
    public bool IsInventoryFull = false;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenDoor(){
        FindObjectOfType<AudioManager>().Play("DoorOpen");
    }


    public void Add(Item item)
    {
        Debug.Log(Items.Count);
        if (Items.Count >= MaxItems)
        {
            Debug.Log("Inventory is full! Cannot add more items.");
            IsInventoryFull = true;
            return;
        }

        Items.Add(item);

        // Instantiate the item in the UI directly
        GameObject obj = Instantiate(InventoryItem, ItemContent);
        var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

        itemName.text = item.itemName;
        itemIcon.sprite = item.icon;


        // Add the item to the InventoryItemController directly
        InventoryItemController itemController = obj.GetComponent<InventoryItemController>();
        itemController.AddItem(item);

        if(Items.Count == MaxItems)
        {
            IsInventoryFull = true;
        }
    }

    public void Remove(Item item)
    {
        Items.Remove(item);

        foreach (Transform child in ItemContent)
        {
            var itemController = child.GetComponent<InventoryItemController>();
            if (itemController != null && itemController.GetItemId() == item.id)
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }
}
