using UnityEngine;

public class DisableBelt : MonoBehaviour
{
    [SerializeField]
    private Inventory Belt;


    public void DisableBeltFunctionality(bool disable)
    {
        if (Belt)
        {
            Belt.DisableInventory(disable);
        }
        else
        {
            Debug.LogError("Belt reference is not set in DisableBelt.");
        }
    }
}
