using UnityEngine;

[CreateAssetMenu(menuName = "ExperimentSteps/ItemInventoryMenuVisualOnly")]
public class ItemInventoryMenuVisualOnly : ItemInventoryMenuStep
{

    public override void ResetStep()
    {
        base.ResetStep();
        DisableSystemsSingleton.Instance.DisableFeedback(true);

    }
 }
