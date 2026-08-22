using System.Collections.Generic;
using UnityEngine;

public class Experiment : ScriptableObject
{
    public string Name;
    public string Description;
    public int NumberOfSteps;
    public List<ExperimentStep> PossibleSteps;

    private List<ExperimentStep> orderedSteps = new List<ExperimentStep>();
    public List<ExperimentStep> OrderedSteps
    {
        get
        {
            return orderedSteps;
        }
    }

    public virtual void SetupExperiment()
    {

    }


    public void generateExperiment()
    {
        orderedSteps.Clear();
        while (orderedSteps.Count < NumberOfSteps)
        {
            int randomIndex = Random.Range(0, PossibleSteps.Count);
            orderedSteps.Add(PossibleSteps[randomIndex]);
        }
    }
}
