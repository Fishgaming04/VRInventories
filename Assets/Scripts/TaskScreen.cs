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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ExerciseSingleton.Instance.OnStepStarted += UpdateScreenStep;
        ExerciseSingleton.Instance.OnExperimentStarted += UpdateScreenExperimentStart;
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
            taskKeeper.StartExperiment();
            isStartExeperiment = false;
        }
        else
        {
            isStartExeperiment = true;
            ExerciseSingleton.Instance.StartStep();
            StartButton.SetActive(false);
        }
    }


}
