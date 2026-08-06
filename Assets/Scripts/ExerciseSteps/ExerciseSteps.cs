using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Health,
    Mana,
    Stamina,
}


//[CreateAssetMenu(menuName = "ExperimentSteps/BaseExperimentStep")]
public class ExperimentStep : ScriptableObject
{
    public List<ActionType> PossibleActions;
    public ActionType RequiredAction;
    public string StepName;
    public Sprite icon;

    public virtual void ResetStep()
    {
        // Reset any step-specific data here
    }
}
