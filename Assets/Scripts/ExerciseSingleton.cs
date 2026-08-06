using System;
using System.Collections.Generic;
using UnityEngine;


public class ExerciseSingleton : MonoBehaviour
{
    #region SINGLETON INSTANCE
    private static ExerciseSingleton _instance;
    public static ExerciseSingleton Instance
    {
        get
        {
            if (_instance == null && !ApplicationQuitting)
            {
                _instance = FindObjectOfType<ExerciseSingleton>();
                if (_instance == null)
                {
                    GameObject newInstance = new GameObject("Singleton_ExerciseSingleton");
                    _instance = newInstance.AddComponent<ExerciseSingleton>();
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
    public event Action<Experiment> OnExperimentStarted;
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
        logger.setup();
        //StartExperiment();
    }

    public void StartExperiment(Experiment currentExperimtent)
    {
        if (currentExperimtent == null)
        {
            LoggerEvent?.Invoke("No experiments available to start.");
            return;
        }
        OnExperimentStarted?.Invoke(currentExperimtent);
        LoggerEvent?.Invoke("Experiment started.");
        if (currentExperimtent.OrderedSteps.Count == 0 || currentExperimtent.OrderedSteps[0] == null)
        {
            currentExperimtent.generateExperiment();
        }
        steps = currentExperimtent.OrderedSteps;
        currentExperimtent.SetupExperiment();
        currentIndex = 0;
        //StartStep();
    }

    public void StartStep()
    {
        //steps[currentIndex].ResetStep();
        OnStepStarted?.Invoke(steps[currentIndex]);
        LoggerEvent?.Invoke($"Step started: {steps[currentIndex].StepName}");
        steps[currentIndex].ResetStep();
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
