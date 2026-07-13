using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    //private int slotID;
    //public int SlotID
    //{
    //    get { return slotID; }
    //}
    [SerializeField]
    private RawImage IconImage;

    private Transform itemSpawnLocation;
    public Transform ItemSpawnLocation
    {
        set { itemSpawnLocation = value; }
    }

    public int ItemAmount;
    private ItemInfo itemInformation;
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


    public void ForceSetItem(ItemInfo itemInfo, int amount)
    {
        this.itemInformation = itemInfo;
        this.ItemAmount = amount;
        IconImage.texture = itemInfo.Icon.texture;
        IconImage.enabled = true;
    }

    public bool SetItem(ItemInfo itemInfo, int amount)
    {
        if (this.itemInformation == null)
        {
            this.itemInformation = itemInfo;
            this.ItemAmount = amount;
            IconImage.texture = itemInfo.Icon.texture;
            IconImage.enabled = true;
            return true;
        }
        else
        {
            //Debug.LogError("Slot is already occupied. Use ForceSetItem to overwrite.");
            return false;
        }
    }

    public void TakeItem()
    {
        if (this.ItemAmount > 0)
        {
            Debug.Log($"Taking item: {itemInformation.Name}");
            this.ItemAmount--;
            Instantiate(itemInformation.Prefab, itemSpawnLocation.position, itemSpawnLocation.rotation);

            if (this.ItemAmount == 0)
            {
                this.itemInformation = null;
                IconImage.texture = null;
                IconImage.enabled = false;
            }
        }
        else
        {
            Debug.LogError("No items to take.");
        }
    }

}
