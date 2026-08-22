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

    private bool isInventoryEnabled = true;
    private void Start()
    {
        SystemInverntoryAcessSignleton.Instance.clearInventory += ClearInventory;
        SystemInverntoryAcessSignleton.Instance.addItem += AddItem;
        SystemInverntoryAcessSignleton.Instance.removeItem += RemoveItemNoReturn;


        foreach (InventorySlot slot in Slots)
        {
            slot.ItemSpawnLocation = ItemSpawnLocation;
        }
    }

    public void DisableInventory(bool disable)
    {
        isInventoryEnabled = !disable;
    }


    public void AddItem(ItemInfo itemInfo)
    {
        AddItem(itemInfo, 1);
    }

    public void AddItem(ItemInfo itemInfo, int amount)
    {
        if (!isInventoryEnabled)
        {
            return;
        }

        if (itemInfo == null)
        {
            Debug.LogError("Cannot add null item to inventory.");
            return;
        }
        // Check if the item already exists in the inventory
        Debug.Log("Invneotry Set potion to " + (itemInfo != null ? itemInfo.Name : "null"));
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

    public void RemoveItemNoReturn(ItemInfo itemInfo)
    {
        RemoveItem(itemInfo);
    }

    public bool RemoveItem(ItemInfo itemInfo)
    {
        if (!isInventoryEnabled)
        {
            return false;
        }
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

    public void ClearInventory()
    {
        foreach (InventorySlot slot in Slots)
        {
            slot.ForceSetItem(null, 0);
            slot.ItemAmount = 0;
        }
    }
}
