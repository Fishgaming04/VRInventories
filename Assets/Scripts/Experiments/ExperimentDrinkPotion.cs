using UnityEngine;


[CreateAssetMenu(menuName = "Experiment/ExperimentDrinkPotion")]
public class ExperimentDrinkPotion : Experiment
{
    public override void SetupExperiment()
    {
        DisableMultiModalFeedbackSingleton.Instance.DisableFeedback(false);
    }
}
