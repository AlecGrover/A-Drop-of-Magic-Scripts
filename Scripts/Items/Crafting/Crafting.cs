using System.Collections.Generic;
using System.Linq;
using UIComponents;
using UnityEngine;

namespace Items.Crafting
{
    public class Crafting : MonoBehaviour, IItemStorage
    {

        [SerializeField]
        private Recipes recipeBook;

        [SerializeField]
        private List<CraftingSlot> craftingSlots;

        [SerializeField]
        private OutputSlot outputSlot;
    
        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        public void Craft()
        {
            if (craftingSlots.Any(slot => slot.holding)) return;
            var inventorySlots = FindObjectsOfType<InventorySlot>();
            if (inventorySlots.Any(slot => slot.holding)) return;
            
            
            
            foreach (var recipe in recipeBook.recipes.Where(recipe => recipe.Craftable(this)))
            {
                outputSlot.GiveItem(recipe.CraftItem(this));
                return;
            }
        }
        

        public bool Contains(Item item)
        {
            return craftingSlots.Any(craftingSlot => craftingSlot.Contains(item));
        }

        public bool TakeItem(Item item)
        {
            foreach (var craftingSlot in craftingSlots)
            {
                if (!craftingSlot.Contains(item)) continue;
                craftingSlot.TakeItem(item);
                return true;
            }

            return false;
        }

        public bool GiveItem(Item item)
        {
            foreach (var craftingSlot in craftingSlots)
            {
                if (!craftingSlot.SpaceAvailable()) continue;
                craftingSlot.GiveItem(item);
                return true;
            }

            return false;
        }

        public bool SpaceAvailable()
        {
            return craftingSlots.Any(craftingSlot => craftingSlot.SpaceAvailable());
        }

        public int CountItem(Item item)
        {
            return craftingSlots.Count(craftingSlot => craftingSlot.Contains(item));
        }

        public void ReturnToInventory()
        {
            var inventoryController = FindObjectOfType<InventoryController>();
            foreach (var slot in craftingSlots.Where(slot => slot.heldItem))
            {
                inventoryController.GiveItem(slot.heldItem);
                slot.DropItem();
            }

            if (!outputSlot.heldItem) return;
            inventoryController.GiveItem(outputSlot.heldItem);
            outputSlot.DropItem();
        }
        
    }
}
