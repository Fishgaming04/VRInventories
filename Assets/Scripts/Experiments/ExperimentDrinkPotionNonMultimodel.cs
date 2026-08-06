using UnityEngine;

[CreateAssetMenu(menuName = "Experiment/ExperimentDrinkPotionNonMultimodel")]
public class ExperimentDrinkPotionNonMultimodel : Experiment
{
    public override void SetupExperiment()
    {
        DisableMultiModalFeedbackSingleton.Instance.DisableFeedback(true);
    }
}