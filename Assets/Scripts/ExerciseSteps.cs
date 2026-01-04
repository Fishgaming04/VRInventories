using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Health,
    Mana,
    Stamina,
}

public class ExperimentStep
{
    public List<ActionType> PossibleActions;
    public ActionType RequiredAction;
    public string InstructionText;

    public virtual void ResetStep()
    {
        // Reset any step-specific data here
    }
}
