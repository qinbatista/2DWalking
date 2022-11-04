using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName="ScriptableObject/User/InventoryDatabase")]
public class InventoryDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] items;
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();
    // Start is called before the first frame update
    public void OnAfterDeserialize()
    {
        for (int i = 0; i< items.Length; i++)
        {
            items[i].Id = i;
            GetItem.Add(i, items[i]);
        }
    }
    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemObject>();
    }
}
