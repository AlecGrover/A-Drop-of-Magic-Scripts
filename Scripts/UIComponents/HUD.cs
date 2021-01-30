using Items.Crafting;
using UnityEngine;

namespace UIComponents
{
    public class HUD : MonoBehaviour
    {

        [SerializeField] private CraftingMenu craftingMenu;
        
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ToggleCraftingMenu()
        {
            var activeCraft = craftingMenu.gameObject.activeSelf;
            Time.timeScale = activeCraft ? 1 : 0;
            if (activeCraft)
            {
                FindObjectOfType<Crafting>().ReturnToInventory();
            }
            craftingMenu.gameObject.SetActive(!activeCraft);
        }
        
    }
}
