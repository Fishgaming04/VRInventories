using UnityEngine;
using UnityEngine.UI;

public class InventorySlotButton : InventorySlot
{
    [SerializeField]
    private RawImage IconImage;


    public override void ForceSetItem(ItemInfo itemInfo, int amount)
    {
        base.ForceSetItem(itemInfo, amount);
        if (itemInfo == null)
        {
            IconImage.enabled = false;
            return;
        }
        IconImage.texture = itemInfo.Icon.texture;
        IconImage.enabled = true;
    }

    public override bool SetItem(ItemInfo itemInfo, int amount)
    {
        Debug.Log("set item buttonbased on base");
        if (base.SetItem(itemInfo, amount))
        {
            IconImage.texture = itemInfo.Icon.texture;
            IconImage.enabled = true;
            return true;
        }
        return false;
    }
    


    public override void TakeItem()
    {
        base.TakeItem();
        if (this.ItemAmount > 0)
        {
            Debug.Log($"Taking item: {itemInformation.Name}");
            this.ItemAmount--;
            Instantiate(itemInformation.Prefab, itemSpawnLocation.position, itemSpawnLocation.rotation);

            if (this.ItemAmount == 0)
            {
                IconImage.texture = null;
                IconImage.enabled = false;
                this.itemInformation = null;
            }
        }
        else
        {
            Debug.LogError("No items to take.");
        }
    }
}
