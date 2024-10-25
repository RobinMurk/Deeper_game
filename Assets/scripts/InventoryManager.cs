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

    public Toggle EnableRemove;
    private const int MaxItems = 4;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        if (Items.Count >= MaxItems)
        {
            Debug.Log("Inventory is full! Cannot add more items.");
            return;
        }

        Items.Add(item);

        // Instantiate the item in the UI directly
        GameObject obj = Instantiate(InventoryItem, ItemContent);
        var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
        var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

        itemName.text = item.itemName;
        itemIcon.sprite = item.icon;

        removeButton.gameObject.SetActive(EnableRemove.isOn);

        // Add the item to the InventoryItemController directly
        InventoryItemController itemController = obj.GetComponent<InventoryItemController>();
        itemController.AddItem(item);
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

    public void EnableItemsRemove()
    {
        foreach (Transform item in ItemContent)
        {
            var removeButton = item.Find("RemoveButton").GetComponent<Button>();
            if (removeButton != null)
                removeButton.gameObject.SetActive(EnableRemove.isOn);
        }
    }
}
