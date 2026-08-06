using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using static UnityEngine.XR.OpenXR.Features.Interactions.HandInteractionProfile;


[RequireComponent(typeof(XRSocketInteractor))]
public class InventorySlotBelt : InventorySlot
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    ///private GameObject itemInSocket;

    //private Collider slotCollider;
    private XRSocketInteractor socketInteractor;


    private void Start()
    {
        //slotCollider = GetComponent<Collider>();
        socketInteractor = GetComponent<XRSocketInteractor>();
        socketInteractor.selectEntered.AddListener(ItemAddedToBelt);
    }

    public override void ForceSetItem(ItemInfo itemInfo, int amount)
    {
        if (amount > 1)
        {
            Debug.LogWarning("The maximum amount is 1");
        }
        base.ForceSetItem(itemInfo, 1);
        if (socketInteractor.hasSelection)
        {
            IXRSelectInteractable selectedItem = socketInteractor.firstInteractableSelected;
            ItemInfo CurrentItemInfo = selectedItem.transform.gameObject.GetComponent<Item>().Info;
            if (CurrentItemInfo != itemInfo)
            {
                socketInteractor.interactionManager.SelectExit(socketInteractor, selectedItem);
                Destroy(selectedItem.transform.gameObject);
                if (itemInfo.Prefab != null)
                {
                    Instantiate(itemInfo.Prefab, socketInteractor.attachTransform);
                }
                return;
            }
        }
        if (itemInfo != null)
        {
            Instantiate(itemInfo.Prefab, socketInteractor.attachTransform);
        }
    }


    public override bool SetItem(ItemInfo itemInfo, int amount)
    {
        if (itemInfo == null)
        {
            Debug.LogWarning("ItemInfo is null. Cannot set item.");
            return false;
        }
        if (amount < 1)
        {
            Debug.LogWarning("The maximum amount is 1");
        }
        if (!socketInteractor.hasSelection)
        {
            //IXRSelectInteractable selectedItem = socketInteractor.firstInteractableSelected;
            //ItemInfo CurrentItemInfo = selectedItem.transform.gameObject.GetComponent<Item>().Info;
           Instantiate(itemInfo.Prefab, socketInteractor.attachTransform);
            base.SetItem(itemInfo, 1);
            
            return true;
        }
        return false;
    }

    public void ItemAddedToBelt(SelectEnterEventArgs args)
    {
        Item item = args.interactableObject.transform.gameObject.GetComponentInChildren<Item>();
        if (item == null)
        {
            Debug.LogWarning("The object added to the belt does not have an Item component.");
            return;
        }
        SystemInverntoryAcessSignleton.Instance.AddItem(item.Info);
    }




    //public void OnCollisionEnter(Collision collision)
    //{
    //    Item item = collision.gameObject.GetComponent<Item>();
    //    if (item == null)
    //    {
    //        Debug.LogWarning("Collided object does not have an Item component.");
    //        return;
    //    }
    //    SystemInverntoryAcessSignleton.Instance.AddItem(item.Info);
    //}

}
