using UnityEngine;

public class BaseAction : MonoBehaviour
{
    [SerializeField]
    private ActionType actionType;

    public void ActionUsed()
    {
        ExerciseSingleton.Instance.SubmitSelection(actionType);
    }

}
