using SerializableCallback;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GrabLogger : MonoBehaviour
{
    public static event Action<string> LoggerEvent;

    [SerializeField]
    private bool isLeftHand = false;

    [SerializeField]
    private InputActionReference TryGrabbing;

    private NearFarInteractor interactor;

    void Start()
    {
        interactor = GetComponent<NearFarInteractor>();
        interactor.selectEntered.AddListener(OnSelectEntered);
        interactor.selectExited.AddListener(OnSelectExited);
        TryGrabbing.action.performed += ctx => TriedGrabbing();
    }

    private void OnSelectExited(SelectExitEventArgs arg)
    {
        Item item = arg.interactableObject.transform.GetComponentInChildren<Item>();
        if (item == null)
        {
            Debug.LogWarning("The released object does not have an Item component.");
            return;
        }
        LoggerEvent?.Invoke($"Item released by {(isLeftHand ? "left" : "right")} hand: {item.name}");
    }

    private void OnSelectEntered(SelectEnterEventArgs arg)
    {

        Item item = arg.interactableObject.transform.GetComponentInChildren<Item>();
        if (item == null)
        {
            Debug.LogWarning("The grabbed object does not have an Item component.");
            return;
        }
        LoggerEvent?.Invoke($"Item grabbed by {(isLeftHand ? "left" : "right")} hand: {item.name}");
    }

    private void TriedGrabbing()
    {
        if (interactor.firstInteractableSelected == null)
        {
            LoggerEvent?.Invoke($"Tried grabbing with {(isLeftHand ? "left" : "right")} hand, but no item was grabbed.");
        }
    }

}
