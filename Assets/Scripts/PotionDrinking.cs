using UnityEngine;


[RequireComponent(typeof(Collider))]
public class PotionDrinking : BaseAction
{
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

    public static bool DrinkWithButtonPress = false;

    private void Start()
    {
        drinkingCollider = GetComponent<Collider>();
        drinkingCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isEmpty || DrinkWithButtonPress)
        {
            return;
        }
        if (other.CompareTag(playerTag))
        {
            ActionUsed();
        }
    }

    public override void ActionUsed()
    {
        if (isEmpty)
        {
            return;
        }
        fullPotionBody.SetActive(false);
        emptyPotionBody.SetActive(true);
        iteminfoHolder.Info = emptyItemInfo;
        isEmpty = true;
        base.ActionUsed();
    }
}
