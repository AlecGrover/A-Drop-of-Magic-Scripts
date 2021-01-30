using System;
using System.Collections.Generic;
using System.Linq;
using UIComponents;
using UnityEngine;

namespace Items.Crafting
{
    [Serializable]
    public struct Ingredient
    {
        public Item item;
        [Range(1, 3)]
        public int count;
    }

    [CreateAssetMenu]
    public class Recipe : ScriptableObject
    {
        public List<Ingredient> ingredients;
        public Item potion;

        public bool Craftable(IItemStorage craftingMenu)
        {
            return ingredients.All(ingredient => craftingMenu.CountItem(ingredient.item) == ingredient.count);
        }

        public Item CraftItem(IItemStorage itemStorage)
        {
            if (!Craftable(itemStorage)) return null;
            foreach (var ingredient in ingredients)
            {
                for (var i = 0; i < ingredient.count; i++) itemStorage.TakeItem(ingredient.item);
            }

            return potion;
        
        }
    
    }
}