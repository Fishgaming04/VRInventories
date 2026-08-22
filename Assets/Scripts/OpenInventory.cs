using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryObjectAttachedToPlayer;
    [SerializeField]
    private GameObject inventoryObjectAttachedInWorld;
    [SerializeField]
    private GameObject locationForInventory;


    private Inventory InventoryAttachedInWorld;
    private Inventory InventoryAttachedToPlayer;

    [SerializeField]
    private List<InputActionReference> openInventory;

    public static bool UseWorldInventory = false;
    public static bool UseInventory = true;

    private bool isInventoryOpen = false;

    public void Start()
    {
        OpenInventoryAction();
        foreach (InputActionReference openInventory in openInventory)
        {
            if (openInventory)
            {
                openInventory.action.performed += ctx => OpenInventoryAction();
            }
        }
    }

    private void OpenInventoryAction()
    {
        if (!inventoryObjectAttachedInWorld || !locationForInventory || !inventoryObjectAttachedToPlayer)
        { 
            Debug.LogWarning("One of the inventory objects is not assigned in the OpenInventory script.");
            return;
        }
        if (isInventoryOpen || !UseInventory)
        {
            CloseInventory();
            isInventoryOpen = false;
            return;
        }
        CloseInventory();
        isInventoryOpen = true;
        if (UseWorldInventory)
        {
            inventoryObjectAttachedInWorld.transform.position = locationForInventory.transform.position;
            inventoryObjectAttachedInWorld.transform.rotation = locationForInventory.transform.rotation;
            inventoryObjectAttachedInWorld.SetActive(true);
            inventoryObjectAttachedToPlayer.SetActive(false);
        }
        else
        {
            inventoryObjectAttachedToPlayer.SetActive(true);
            inventoryObjectAttachedInWorld.SetActive(false);
        }
    }

    public void CloseInventory()
    {
        if (inventoryObjectAttachedInWorld && inventoryObjectAttachedToPlayer)
        {
            disableInventory();
            inventoryObjectAttachedInWorld.SetActive(false);
            inventoryObjectAttachedToPlayer.SetActive(false);
        }
        else
        {
            Debug.LogError("Inventory objects are not assigned in the OpenInventory script.");
        }
    }


    private void disableInventory()
    {
        if (!inventoryObjectAttachedInWorld || !inventoryObjectAttachedToPlayer)
        {
            Debug.LogWarning("One of the inventory objects is not assigned in the OpenInventory script.");
            return;
        }
        if (!InventoryAttachedInWorld || !InventoryAttachedToPlayer)
        {
            InventoryAttachedInWorld = inventoryObjectAttachedInWorld.GetComponent<Inventory>();
            InventoryAttachedToPlayer = inventoryObjectAttachedToPlayer.GetComponent<Inventory>();
        }
        InventoryAttachedInWorld.DisableInventory(!UseInventory);
        InventoryAttachedToPlayer.DisableInventory(!UseInventory);

    }
}

