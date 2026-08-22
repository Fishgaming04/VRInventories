using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Feedback;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class DisableSystemsSingleton
{
    #region SINGLETON INSTANCE
    private static DisableSystemsSingleton _instance;
    public static DisableSystemsSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DisableSystemsSingleton();
            }

            return _instance;
        }
    }
    //Checks if the singleton is alive, useful to reference it when the game is about to close down to avoid memory leaks
    public static bool Exists
    {
        get
        {
            return _instance != null;
        }
    }
    public static bool ApplicationQuitting = false;
    protected virtual void OnApplicationQuit()
    {
        ApplicationQuitting = true;
    }
    #endregion

    private List<AudioSource> audioSources = new List<AudioSource>();
    private List<HapticImpulsePlayer> haptics = new List<HapticImpulsePlayer>();
    private List<SimpleHapticFeedback> simpleHaptics = new List<SimpleHapticFeedback>();

    private DisableBelt disableBelt;
    private List<OpenInventory> OpenInventoryComponents = new List<OpenInventory>();

    private bool isFeedbackDisabled = false;

    public void findAllFeedbackSources()
    {
        audioSources.Clear();
        haptics.Clear();
        simpleHaptics.Clear();

        audioSources = new List<AudioSource>(GameObject.FindObjectsOfType<AudioSource>());
        haptics = new List<HapticImpulsePlayer>(GameObject.FindObjectsOfType<HapticImpulsePlayer>());
        simpleHaptics = new List<SimpleHapticFeedback>(GameObject.FindObjectsOfType<SimpleHapticFeedback>());

        //inventoryGestureInteraction.Clear();

        disableBelt = GameObject.FindObjectOfType<DisableBelt>();
        OpenInventoryComponents = new List<OpenInventory>(GameObject.FindObjectsOfType<OpenInventory>());
    
        
    
    }

    public void DisableFeedback(bool disable)
    {
        if (disable == isFeedbackDisabled)
        {
            return; // No change needed
        }
        isFeedbackDisabled = disable;
        if (audioSources == null || haptics == null)
        {
            findAllFeedbackSources();
        }
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = isFeedbackDisabled;
        }
        foreach (HapticImpulsePlayer haptic in haptics)
        {
            haptic.enabled = !isFeedbackDisabled;
        }
        foreach (SimpleHapticFeedback simpleHaptic in simpleHaptics)
        {
            simpleHaptic.enabled = !isFeedbackDisabled;
        }
    }

    public void DisableButtonPresses(bool disable)
    {
        UseHeldItemWithButton.disableUseItem = disable;
        PotionDrinking.DrinkWithButtonPress = !disable;
    }

    public void UseBeltInventory(bool enable)
    {
        if (disableBelt == null)
        {
            findAllFeedbackSources();
        }
        if (disableBelt)
        {
            disableBelt.DisableBeltFunctionality(!enable);
            OpenInventory.UseInventory = !enable;

            closeInvenotoryPanels();
            //foreach (InventoryGestureInteraction inventory in inventoryGestureInteraction) {
            //    inventory.gameObject.SetActive(enable);
            //}
        }
        else
        {
            Debug.LogWarning("No DisableBelt component found in the scene.");
        }
    }

    public void closeInvenotoryPanels()
    {
        if (OpenInventoryComponents.Count > 0)
        {
            foreach(OpenInventory inventory in OpenInventoryComponents)
            {
                inventory.CloseInventory();
            }
        }
        else
        {
            findAllFeedbackSources();
            closeInvenotoryPanels();
        }
    }

}
