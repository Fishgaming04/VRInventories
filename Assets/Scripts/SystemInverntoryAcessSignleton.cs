using System;
using UnityEngine;

public class SystemInverntoryAcessSignleton 
{
    public event Action clearInventory;
    public event Action<ItemInfo> addItem;
    public event Action<ItemInfo> removeItem;


    #region SINGLETON INSTANCE
    private static SystemInverntoryAcessSignleton _instance;
    public static SystemInverntoryAcessSignleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SystemInverntoryAcessSignleton();
            }

            return _instance;
        }
    }
    //Checks if the singleton is alive, useful to reference it when the game is about to close down to avoid memory leaks
    public static bool Exists
    {
        get
        {
            return _instance != null;
        }
    }
    public static bool ApplicationQuitting = false;
    protected virtual void OnApplicationQuit()
    {
        ApplicationQuitting = true;
    }
    #endregion

    public void ClearInventory()
    {
        clearInventory?.Invoke();
    }
    public void AddItem(ItemInfo item)
    {
        addItem?.Invoke(item);
    }
    public void RemoveItem(ItemInfo item)
    {
        removeItem?.Invoke(item);
    }
}
