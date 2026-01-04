using System;
using System.Collections.Generic;
using UnityEngine;


public class ExperimentSingleton : MonoBehaviour
{
    #region SINGLETON INSTANCE
    private static ExperimentSingleton _instance;
    public static ExperimentSingleton Instance
    {
        get
        {
            if (_instance == null && !ApplicationQuitting)
            {
                _instance = FindObjectOfType<ExperimentSingleton>();
                if (_instance == null)
                {
                    GameObject newInstance = new GameObject("Singleton_ExerciseSingleton");
                    _instance = newInstance.AddComponent<ExperimentSingleton>();
                }
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

    public event Action<ExperimentStep> OnStepStarted;
    public event Action<float> OnStepCompleted;


    private List<ExperimentStep> steps;
    private int currentIndex;
    private float stepStartTime;

    public void StartExperiment(List<ExperimentStep> orderedSteps)
    {
        steps = orderedSteps;
        currentIndex = 0;
        StartStep();
    }

    private void StartStep()
    {
        steps[currentIndex].ResetStep();
        OnStepStarted?.Invoke(steps[currentIndex]);
        stepStartTime = Time.time;
    }

    public void SubmitSelection(ActionType action)
    {
        ExperimentStep currentStep = steps[currentIndex];

        if (action != currentStep.RequiredAction)
        {
            return;
        }

        float reactionTime = Time.time - stepStartTime;
        OnStepCompleted?.Invoke(reactionTime);

        currentIndex++;
        if (currentIndex < steps.Count)
        {
            StartStep();
        }
    }

}
