using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using PlayerComponents;
using UnityEngine;

namespace UIComponents
{
    public class InventoryController : MonoBehaviour, IItemStorage
    {
        
        // TODO: Remove commented legacy code if current system is functional 
        
        [SerializeField] private InventorySlot[] inventorySlots = new InventorySlot[6];
        // private List<Item> _items = new List<Item>();

        private void Start()
        {
            var player = FindObjectOfType<Player>();
            if (player) player.inventoryController = this;
        }


        public bool PickUpItem(Item item)
        {
            return GiveItem(item);
            // if (_items.Count >= inventorySlots.Length) return false;
            // _items.Add(item);
            // return inventorySlots[_items.Count - 1].AddHeldItem(item);
        }

        public bool DropItemBySlot(int slot)
        {
            return inventorySlots[slot].DropItem();
            // var count = _items.Count;
            // if (slot < 0 || slot >= count) return false;
            // _items.RemoveAt(slot);
            // count--;
            // bool success = true;
            // for (var i = 0; i < _items.Count; i++)
            // {
            //     inventorySlots[i].RemoveItem();
            //     if (!inventorySlots[i].AddHeldItem(_items[i])) success = false;
            // }
            // inventorySlots[count].RemoveItem();
            // return success;
        }

        public Item TakeItemBySlot(int slot)
        {
            if (slot < 0 || slot >= inventorySlots.Length) return null;
            return inventorySlots[slot].RemoveHeldItem();
        }

        public bool Contains(Item item)
        {
            return inventorySlots.Any(slot => slot.Contains(item));
        }

        public bool TakeItem(Item item)
        {
            foreach (var inventorySlot in inventorySlots)
            {
                if (!inventorySlot.Contains(item)) continue;
                inventorySlot.TakeItem(item);
                return true;
            }

            return false;
            
            // _items.Remove(item);
            // var count = _items.Count;
            // bool success = true;
            // for (var i = 0; i < _items.Count; i++)
            // {
            //     inventorySlots[i].RemoveItem();
            //     if (!inventorySlots[i].AddHeldItem(_items[i])) success = false;
            // }
            // inventorySlots[count].RemoveItem();
            // return success;
        }

        public bool GiveItem(Item item)
        {
            foreach (var inventorySlot in inventorySlots)
            {
                if (!inventorySlot.SpaceAvailable()) continue;
                inventorySlot.GiveItem(item);
                return true;
            }

            return false;
        }

        public bool SpaceAvailable()
        {
            return inventorySlots.Any(slot => slot.SpaceAvailable());
        }

        public int CountItem(Item item)
        {
            return inventorySlots.Count(slot => slot.Contains(item));
        }
    }
}
