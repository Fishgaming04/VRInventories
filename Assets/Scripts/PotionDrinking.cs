using UnityEngine;


[RequireComponent(typeof(Collider))]
public class PotionDrinking : MonoBehaviour
{
    [SerializeField]
    private BaseAction action;

    [SerializeField]
    private GameObject fullPotionBody;

    [SerializeField]
    private GameObject emptyPotionBody;

    [SerializeField]
    private Item iteminfoHolder;

    [SerializeField]
    private ItemInfo emptyItemInfo;

    private bool isEmpty = false;
    private Collider drinkingCollider;
    private const string playerTag = "MainCamera";

    private void Start()
    {
        drinkingCollider = GetComponent<Collider>();
        drinkingCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isEmpty)
        {
            return;
        }
        if (other.CompareTag(playerTag))
        {
            fullPotionBody.SetActive(false);
            emptyPotionBody.SetActive(true);
            iteminfoHolder.Info = emptyItemInfo;
            isEmpty = true;
            action.ActionUsed();
        }
    }


}
