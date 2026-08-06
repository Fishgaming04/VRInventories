using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class DisableMultiModalFeedbackSingleton
{
    #region SINGLETON INSTANCE
    private static DisableMultiModalFeedbackSingleton _instance;
    public static DisableMultiModalFeedbackSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DisableMultiModalFeedbackSingleton();
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

    private List<AudioSource> audioSources;
    private List<HapticImpulsePlayer> haptics;

    private bool isFeedbackDisabled = false;

    public void findAllFeedbackSources()
    {
        audioSources.Clear();
        haptics.Clear();

        audioSources = new List<AudioSource>(GameObject.FindObjectsOfType<AudioSource>());
        haptics = new List<HapticImpulsePlayer>(GameObject.FindObjectsOfType<HapticImpulsePlayer>());
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
    }



}
