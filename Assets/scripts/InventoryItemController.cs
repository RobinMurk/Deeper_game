using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    public Button RemoveButton;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

    }
    public int GetItemId()
    {
        if (item == null)
        {
            Debug.LogError("Item is null in InventoryItemController!");
            return -1; // Return a default value or handle it as needed
        }
        return item.id; // Assuming item.id is valid
    }


    public void UseItem() 
    {
        switch (item.itemType) 
        {
            case Item.ItemType.Potion:
                Player.Instance.IncreaseHealth(item.value); 
                break;
            case Item.ItemType.Book:
                Player.Instance.IncreaseExp(item.value);
                break;
        }
        RemoveItem();
    }   
}
