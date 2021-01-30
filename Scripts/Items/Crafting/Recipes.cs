using System.Collections.Generic;
using UnityEngine;

namespace Items.Crafting
{
    [CreateAssetMenu]
    public class Recipes : ScriptableObject
    {
        public List<Recipe> recipes;
    }
}
