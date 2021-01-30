using UnityEngine;

namespace UIComponents
{
    public class CraftingMenu : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ToggleMenu()
        {
            var hud = FindObjectOfType<HUD>();
            if (hud) hud.ToggleCraftingMenu();
        }
    

    }
}
