using UnityEngine;

public class BaseAction : MonoBehaviour
{
    [SerializeField]
    private ActionType actionType;

    public virtual void ActionUsed()
    {
        ExerciseSingleton.Instance.SubmitSelection(actionType);
    }

}
