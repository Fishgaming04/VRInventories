using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    public static event Action<string> LoggerEvent;

    public int ItemAmount;
    protected ItemInfo itemInformation;

    protected Transform itemSpawnLocation;
    public Transform ItemSpawnLocation
    {
        set { itemSpawnLocation = value; }
    }

    #region operators

    public static bool operator ==(InventorySlot a, InventorySlot b)
    {
        return a.itemInformation.Name == b.itemInformation.Name;
    }

    public static bool operator ==(InventorySlot a, ItemInfo b)
    {
        if (a.itemInformation == null || b == null)
        {
            return false;
        }
        return a.itemInformation.Name == b.Name;
    }

    public static bool operator !=(InventorySlot a, InventorySlot b)
    {
        return a.itemInformation.Name != b.itemInformation.Name;
    }

    public static bool operator !=(InventorySlot a, ItemInfo b)
    {
        if (a.itemInformation == null || b == null)
        {
            Debug.LogWarning("Comparing null InventorySlot or null ItemInfo. Returning true for != operator.");
            return true;
        }
        return a.itemInformation.Name != b.Name;
    }

    public static implicit operator bool(InventorySlot a)
    {
        return a.itemInformation == null;
    }

    #endregion


    public virtual void ForceSetItem(ItemInfo itemInfo, int amount)
    {
        if (itemInfo == null)
        {
            amount = 0;
        }
        else
        {
            LoggerEvent?.Invoke($"Forcing item: {itemInfo.Name} with amount: {amount}");
        }
        this.ItemAmount = amount;
        this.itemInformation = itemInfo;
    }

    public virtual bool SetItem(ItemInfo itemInfo, int amount)
    {
        if (this.itemInformation == null)
        {
            this.itemInformation = itemInfo;
            this.ItemAmount = amount;
            LoggerEvent?.Invoke($"Setting item: {itemInformation.Name} with amount: {ItemAmount}");
            return true;
        }
        else
        {
            //Debug.LogError("Slot is already occupied. Use ForceSetItem to overwrite.");
            return false;
        }
    }

    public virtual void TakeItem()
    {
        if (this.itemInformation == null)
        {
            return;
        }
        LoggerEvent?.Invoke($"Taking item: {itemInformation.Name}");
        if (ItemAmount <= 0)
        {
            itemInformation = null;
        }   
    }

}
