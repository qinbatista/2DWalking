using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObject/Items/Scroll")]
public  class ScrollObject : ItemObject
{
    // public GameObject prefab;
    // public int Id;
    // public bool stackable;
    // public int Id;
    // public ItemType itemType;
    // public string itemName;
    // [TextArea(5,10)]
    // public string description;
    private void Awake()
    {
        itemType = ItemType.Scroll;    
        stackable = false;
    }
}
[System.Serializable]
public class ScrollData
{

    public bool stackable;
    public string itemName;
    public string description;
    public int Id;
    public ScrollData(ScrollData item)
    {
        Debug.Log("Item=");
        // Id = item.Id;
        // stackable = item.stackable;
        // itemType = item.itemType;
        itemName = item.itemName;
        description = item.description;
        // weaponElementType = (WeaponElementType)Random.Range(0, System.Enum.GetValues(typeof(WeaponElementType)).Length);
        // itemAttribute = new ItemAttribute[item.itemAttribute.Length];
        // for (int i =0; i<itemAttribute.Length; i++)
        //     itemAttribute[i] = new ItemAttribute(item.itemType, item.weaponType);
    }
}