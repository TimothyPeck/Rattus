using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace Rattus
//{
    public class Inventory : MonoBehaviour
    {
        private const int INVENTORY_SIZE = 6;
        private List<string> inventoryList = new List<string>();

    /// <summary>
    /// Adds an item to the inventory list
    /// </summary>
    /// <param name="obj">The object to add to the inventory</param>
        public void addItemToInventory(Object obj)
        {
            if (inventoryList.Count < INVENTORY_SIZE)
            {
                inventoryList.Add(obj.name);
                sortInventory();
            }
        }

    /// <summary>
    /// removes an item from the inventory
    /// </summary>
    /// <param name="obj">The object to remove from the inventory</param>
        public void removeFromInventory(Object obj)
        {
            inventoryList.Remove(obj.name);
            sortInventory();
        }

    /// <summary>
    /// <see cref="removeFromInventory(Object)"/> but with string
    /// </summary>
    /// <param name="name">The name of the object to remove from the inventory</param>
        public void removeFromInventory(string name)
        {
            inventoryList.Remove(name);
            sortInventory();
        }

    /// <summary>
    /// Sorts the inventory by name
    /// </summary>
        private void sortInventory()
        {
            inventoryList.Sort();
        }

    /// <summary>
    /// Removes all values from the inventory list
    /// </summary>
        public void emptyInventory()
        {
            inventoryList.Clear();
        }

    /// <summary>
    /// Returns the current inventory
    /// </summary>
    /// <returns>List\<String\> of the current inventory</returns>
        public List<string> getInventory()
        {
            return inventoryList;
        }

    /// <summary>
    /// Checks if the inventory contains an item with the given name
    /// </summary>
    /// <param name="itemName">name of the item to look for</param>
    /// <returns>bool, true if found, false if not</returns>
        public bool containsItem(string itemName)
        {
            return inventoryList.Contains(itemName);
        }
    }
//}