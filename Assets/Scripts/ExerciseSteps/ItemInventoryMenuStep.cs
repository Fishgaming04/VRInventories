using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ExperimentSteps/ItemInventoryMenuStep")]
public class ItemInventoryMenuStep : ExperimentStep
{
    [SerializeField]
    private List<ItemInfo> Items;

    public override void ResetStep()
    {
        SystemInverntoryAcessSignleton.Instance.ClearInventory();
        List<ItemInfo> shuffled = Items;

        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
        }

        foreach (ItemInfo item in shuffled)
        {
            SystemInverntoryAcessSignleton.Instance.AddItem(item);
        }
    }
}
