using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    public void OnMouseDown()
    {
        Pickup();
    }
}
