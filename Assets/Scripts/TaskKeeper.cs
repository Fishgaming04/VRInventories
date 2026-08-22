using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TaskKeeper : MonoBehaviour
{

    [SerializeField]
    public List<Experiment> Experiments;
    [SerializeField]
    public int ExperimentIndex = 0;

    void Start()
    {
        if (Experiments == null || Experiments.Count == 0)
        {
            Debug.LogError("No experiments assigned in TaskKeeper.");
            return;
        }
        if (ExperimentIndex < 0 || ExperimentIndex >= Experiments.Count)
        {
            Debug.LogError("ExperimentIndex is out of bounds.");
            return;
        }

        for (int i = Experiments.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            Experiment temp = Experiments[i];
            Experiments[i] = Experiments[randomIndex];
            Experiments[randomIndex] = temp;
        }
    }



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
