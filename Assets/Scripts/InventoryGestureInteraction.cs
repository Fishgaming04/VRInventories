using UnityEngine;

[RequireComponent (typeof(Collider))]

public class InventoryGestureInteraction : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    private Collider interactionCollider;

    private void Start()
    {
        interactionCollider = GetComponent<Collider>(); 
        interactionCollider.isTrigger = true;
    }


    public void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            Debug.Log($"Item {item.Info.Name} collected!");
            inventory.AddItem(item.Info, 1);
            Destroy(other.gameObject);
        }
    }
}
