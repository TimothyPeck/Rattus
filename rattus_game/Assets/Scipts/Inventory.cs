using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rattus.Inventory
{
    public class Inventory : MonoBehaviour
    {
        private const int INVENTORY_SIZE = 6;
        private List<string> inventoryList = new List<string>();

        public void addItemToInventory(Object obj)
        {
            if (inventoryList.Count < INVENTORY_SIZE)
            {
                inventoryList.Add(obj.name);
                sortInventory();
            }
        }

        public void removeFromInventory(Object obj)
        {
            inventoryList.Remove(obj.name);
            sortInventory();
        }

        private void sortInventory()
        {
            inventoryList.Sort();
        }

        public void emptyInventory()
        {
            inventoryList.Clear();
        }

        public List<string> getInventory()
        {
            return inventoryList;
        }

        public bool containsItem(string itemName)
        {
            return inventoryList.Contains(itemName);
        }
    }
}