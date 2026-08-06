using UnityEngine;

[CreateAssetMenu(menuName = "ExperimentSteps/ItemInventoryMenuVisualOnly")]
public class ItemInventoryMenuVisualOnly : ItemInventoryMenuStep
{

    public override void ResetStep()
    {
        base.ResetStep();
        DisableMultiModalFeedbackSingleton.Instance.DisableFeedback(true);

    }
 }
