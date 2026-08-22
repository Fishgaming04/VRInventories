using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class UseHeldItemWithButton : MonoBehaviour
{
    [SerializeField]
    private XRBaseInteractor interactor;

    public static bool disableUseItem = false;

    [SerializeField]
    private InputActionReference useItemAction;

    private void Start()
    {
        if (useItemAction)
        {
            useItemAction.action.performed += ctx => UseHeldItem();
        }
        else
        {
            Debug.LogError("Use Item Action is not assigned in UseHeldItemWithButton.");
        }
    }



    public void UseHeldItem()
    {
        if (disableUseItem)
        {
            return;
        }
        if (interactor.hasSelection)
        {
            IXRSelectInteractable heldItem = interactor.firstInteractableSelected;
            if (heldItem != null)
            {
                BaseAction action = heldItem.transform.GetComponentInChildren<BaseAction>();
                if (action)
                {
                    action.ActionUsed();
                }
                else
                {
                    Debug.LogWarning("Held item does have BaseAction.");
                }
            }
        }
    }
}
