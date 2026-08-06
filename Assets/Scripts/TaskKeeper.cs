using System.Collections.Generic;
using UnityEngine;

public class TaskKeeper : MonoBehaviour
{

    [SerializeField]
    public List<Experiment> Experiments;
    [SerializeField]
    public int ExperimentIndex = 0;


    public void StartExperiment()
    {
        if (Experiments == null || Experiments.Count == 0)
        {
            Debug.LogError("No experiments available to start.");
            return;
        }
        if (ExperimentIndex >= Experiments.Count)
        {
            Debug.LogError("All experiments have been completed.");
            return;
        }

        ExerciseSingleton.Instance.StartExperiment(Experiments[ExperimentIndex]);
        ExperimentIndex++;
    }
}
