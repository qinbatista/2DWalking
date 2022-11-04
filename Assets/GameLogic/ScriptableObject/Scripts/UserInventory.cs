using System.Security.Principal;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
[CreateAssetMenu(menuName="ScriptableObject/User/UserInventory")]
public class UserInventory : ScriptableObject
{
   public string savePath="";
   public Inventory Container;
   public void AddWeapon(WeaponData weaponData)
   {
      bool hasItem = false;
      // Debug.Log("item.stackable="+item.stackable);
      // Debug.Log("item.ID="+item.Id);
      // Debug.Log("item.name="+item.itemName);
      if(weaponData.stackable)
      {
         for(int i =0; i<Container.WeaponContainer.Count; i++)
         {
            // Debug.Log("Container.WeaponContainer[i].item="+Container.WeaponContainer[i].item);
            // Debug.Log("item="+item);
            if(Container.WeaponContainer[i].weaponData.Id == weaponData.Id)
            {
               Container.WeaponContainer[i].AddAmount(1);
               hasItem = true;
               break;
            }
         }
         if(!hasItem)
         {
            Container.WeaponContainer.Add(new InventoryWeaponSlot(weaponData, 1));
         }
      }
      else
      {
         Container.WeaponContainer.Add(new InventoryWeaponSlot(weaponData, 1));
      }
   }
   public void AddScroll(ScrollData scrollData)
   {
      bool hasItem = false;
      if(scrollData.stackable)
      {
         for(int i =0; i<Container.ScrollContainer.Count; i++)
         {
            if(Container.ScrollContainer[i].scrollData == scrollData)
            {
               Container.ScrollContainer[i].AddAmount(1);
               hasItem = true;
               break;
            }
         }
         if(!hasItem)
         {
            Container.ScrollContainer.Add(new InventoryScrollSlot(scrollData, 1));
         }
      }
      else
      {
         Container.ScrollContainer.Add(new InventoryScrollSlot(scrollData, 1));
      }
   }
   [ContextMenu("save")]
   public void Save()
   {
      // Debug.Log(string.Concat(Application.persistentDataPath,savePath));
      IFormatter formatter = new BinaryFormatter();
      Stream stream = new FileStream(string.Concat(Application.persistentDataPath,savePath),FileMode.Create,FileAccess.Write);
      formatter.Serialize(stream, Container);
      stream.Close();
   }
   [ContextMenu("load")]
   public void Load()
   {
      if(File.Exists(string.Concat(Application.persistentDataPath,savePath)))
      {
         IFormatter formatter = new BinaryFormatter();
         // Debug.Log(string.Concat(Application.persistentDataPath,savePath));
         Stream stream = new FileStream(string.Concat(Application.persistentDataPath,savePath),FileMode.Open,FileAccess.Read);
         // Debug.Log(stream.Length);
         Container = (Inventory)formatter.Deserialize(stream);
         stream.Close();
      }
   }
   [ContextMenu("clean")]
   public void Clean()
   {
      Container = new Inventory();
   }
}
[System.Serializable]
public class Inventory
{
   public List<InventoryWeaponSlot> WeaponContainer = new List<InventoryWeaponSlot>();
   public List<InventoryScrollSlot> ScrollContainer = new List<InventoryScrollSlot>();
}
[System.Serializable]
public class InventoryWeaponSlot
{
   public int amount = 0;
   public WeaponData weaponData;
   // public InventoryWeaponSlot(ScrollData _scrollData, int _amount)
   // {
   //    // Debug.Log("item");
   //    amount = _amount;
   //    scrollData = _scrollData;
      
   // }
   public InventoryWeaponSlot(WeaponData _weaponData, int _amount)
   {
      amount = _amount;
      weaponData = _weaponData;
      // itemData.Id = _weaponData.Id;
      // itemData.itemName = _weaponData.itemName;
      
   }
   public void AddAmount(int value)
   {
      amount = amount+1;
   }
}
[System.Serializable]
public class InventoryScrollSlot
{
   public int amount = 0;
   public ScrollData scrollData;
   public InventoryScrollSlot(ScrollData _scrollData, int _amount)
   {
      amount = _amount;
      scrollData = _scrollData;
   }
   public void AddAmount(int value)
   {
      amount = amount+1;
   }
}
// public class InventoryS