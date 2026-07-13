using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //[SerializeField]
    //const int InventorySize = 20;

    [SerializeField]
    private List<InventorySlot> Slots;


    [SerializeField]
    private Transform ItemSpawnLocation;


    private void Start()
    {
        foreach (InventorySlot slot in Slots)
        {
            slot.ItemSpawnLocation = ItemSpawnLocation;
        }
    }


    public void AddItem(ItemInfo itemInfo, int amount)
    {
        if (itemInfo == null)
        {
            Debug.LogError("Cannot add null item to inventory.");
            return;
        }
        // Check if the item already exists in the inventory
        foreach (InventorySlot slot in Slots)
        {
            if (slot == itemInfo)
            {
                slot.ItemAmount += amount;
                return;
            }
        }
        // If the item doesn't exist, find an empty slot
        foreach (InventorySlot slot in Slots)
        {
            if (slot)
            {
                if (!slot.SetItem(itemInfo, amount))
                {
                    Debug.LogError("Failed to add item to inventory: " + itemInfo.Name);
                }
                return;
            }
        }
    }

    public bool RemoveItem(ItemInfo itemInfo)
    {
        foreach (InventorySlot slot in Slots)
        {
            if (slot == itemInfo)
            {
                slot.ItemAmount--;
                if (slot.ItemAmount <= 0)
                {
                    slot.ForceSetItem(null, 0);
                    slot.ItemAmount = 0;
                }
                return true;
            }
        }
       return false;
    }
}
