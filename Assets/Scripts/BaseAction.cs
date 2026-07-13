using UnityEngine;

public class BaseAction : MonoBehaviour
{
    [SerializeField]
    private ActionType actionType;

    public void ActionUsed()
    {
        ExperimentSingleton.Instance.SubmitSelection(actionType);
    }

}
