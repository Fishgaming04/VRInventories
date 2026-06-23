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
    public event Action OnExperimentStarted;
    public event Action<ExperimentStep> OnStepStarted;
    public event Action<float> OnStepCompleted;
    public event Action WrongActionSelected;

    public event Action<string> LoggerEvent;

    private List<ExperimentStep> steps;
    private int currentIndex;
    private float stepStartTime;


    private void Start()
    {
        Debug.Log("ExperimentSingleton started.");
        Logger logger = new Logger();
        logger.stetup();
    }


    public void StartExperiment(List<ExperimentStep> orderedSteps)
    {
        OnExperimentStarted?.Invoke();
        LoggerEvent?.Invoke("Experiment started.");
        steps = orderedSteps;
        currentIndex = 0;
        StartStep();
    }

    private void StartStep()
    {
        steps[currentIndex].ResetStep();
        OnStepStarted?.Invoke(steps[currentIndex]);
        LoggerEvent?.Invoke($"Step started: {steps[currentIndex].StepName}");
        stepStartTime = Time.time;
    }

    public void SubmitSelection(ActionType action)
    {
        ExperimentStep currentStep = steps[currentIndex];

        if (action != currentStep.RequiredAction)
        {
            LoggerEvent?.Invoke($"Wrong action selected: {action}. Required: {currentStep.RequiredAction}");
            return;
        }

        float reactionTime = Time.time - stepStartTime;
        OnStepCompleted?.Invoke(reactionTime);
        LoggerEvent?.Invoke($"Step completed: {currentStep.StepName}. Reaction time: {reactionTime} seconds.");

        currentIndex++;
        if (currentIndex < steps.Count)
        {
            StartStep();
        }
    }




}
