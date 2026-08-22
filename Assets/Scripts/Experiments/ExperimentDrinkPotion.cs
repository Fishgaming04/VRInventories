using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Experiment/ExperimentDrinkPotion")]
public class ExperimentDrinkPotion : Experiment
{
    [SerializeField]
    private bool useBeltInventory = false;
    [SerializeField]
    private bool MultimodelEnabled = false;
    [SerializeField]
    private bool ButtonPressItemUse = false;

    public override void SetupExperiment()
    {
        DisableSystemsSingleton.Instance.DisableFeedback(!MultimodelEnabled);
        DisableSystemsSingleton.Instance.DisableButtonPresses(!ButtonPressItemUse);
        DisableSystemsSingleton.Instance.UseBeltInventory(useBeltInventory);

        //List<Item> items = new List<Item>(GameObject.FindObjectsOfType<Item>());

        //foreach (Item item in items)
        //{
        //    Destroy(item.transform.parent.gameObject);
        //}
    }
}
