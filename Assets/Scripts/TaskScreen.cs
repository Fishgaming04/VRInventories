using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI taskDisplay;

    [SerializeField]
    private RawImage taskImage;

    [SerializeField]
    private GameObject StartButton;

    [SerializeField]
    private TaskKeeper taskKeeper;

    private bool isStartExeperiment = true;


    private string startNextExperimentText = "Press Start to begin the next experiment";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ExerciseSingleton.Instance.OnStepStarted += UpdateScreenStep;
        ExerciseSingleton.Instance.OnExperimentStarted += UpdateScreenExperimentStart;
        ExerciseSingleton.Instance.OnExperimentEnded += ExperimentEnded;
    }

    private void UpdateScreenStep(ExperimentStep step)
    {
        if (step == null)
        {
            taskImage.enabled = false;
            taskDisplay.enabled = false;
            StartButton.SetActive(true);
            return;
        }
        //if (step.InstructionText != null)
        //{
        //    taskDisplay.enabled = true;
        //    taskImage.enabled = false;
        //    taskDisplay.text = step.InstructionText;
        //}
        else if (step.icon != null)
        {
            taskDisplay.enabled = false;
            taskImage.enabled = true;
            taskImage.texture = step.icon.texture;
        }
    }


    private void UpdateScreenExperimentStart(Experiment experiment)
    {
        if (experiment == null)
        {
            taskImage.enabled = false;
            taskDisplay.enabled = false;
            StartButton.SetActive(true);
            return;
        }
        if (experiment.Description != null)
        {
            taskDisplay.enabled = true;
            taskImage.enabled = false;
            taskDisplay.text = experiment.Description;
        }
    }

    public void ButtonPressed()
    {
        if (isStartExeperiment)
        {
            if (taskKeeper == null)
            {
                Debug.LogError("TaskKeeper reference is not set in TaskScreen.");
                return;
            }
            isStartExeperiment = false;
            taskKeeper.StartExperiment();
        }
        else
        {
            isStartExeperiment = true;
            StartButton.SetActive(false);
            ExerciseSingleton.Instance.StartStep();
        }
    }

    public void ExperimentEnded()
    {
        isStartExeperiment = true;
        StartButton.SetActive(true);
        taskImage.enabled = false;
        taskDisplay.enabled = true;
        taskDisplay.text = startNextExperimentText;
    }
}
