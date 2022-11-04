using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum ItemType
{
    Weapon,
    Scroll 
}
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public int Id;
    public bool stackable;
    public ItemType itemType;
    public string itemName;
    [TextArea(5,10)]
    public string description;
}
// [System.Serializable]
// public class ItemData
// {
//     public int Id;
//     public bool stackable;
//     public ItemType itemType;
//     public string itemName;
//     public string description;
//     public ItemData(ItemObject item)
//     {
//         Debug.Log("Item=");
//         Id = item.Id;
//         stackable = item.stackable;
//         itemType = item.itemType;
//         itemName = item.itemName;
//         description = item.description;
//         weaponElementType = (WeaponElementType)Random.Range(0, System.Enum.GetValues(typeof(WeaponElementType)).Length);
//         itemAttribute = new ItemAttribute[item.itemAttribute.Length];
//         for (int i =0; i<itemAttribute.Length; i++)
//             itemAttribute[i] = new ItemAttribute(item.itemType, item.weaponType);
//     }
// }
// [System.Serializable]
// public class ItemAttribute
// {
//     public ItemAttribute(ItemType itemType, WeaponType weaponType)
//     {
//         if(itemType == ItemType.Weapon)
//         {
//             switch (weaponType)
//             {
//                 case WeaponType.Laser:
//                     break;
//                 case WeaponType.Orb:
//                     break;    
//                 default:
//                     break;
//             }
            
//         }
//     }
//     public void GenerateElementType()
//     {

//     }
// }