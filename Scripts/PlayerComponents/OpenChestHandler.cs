using UnityEngine;

namespace PlayerComponents
{
    public class OpenChestHandler : MonoBehaviour
    {

        [SerializeField] private Interact interact;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OpenChest()
        {
            interact.TriggerInteract();
        }

        public void CloseChest()
        {
            interact.TriggerInteract();
        }

        public void EndInteraction()
        {
            interact.EndInteraction();
        }

        public void AddToInventory()
        {
            interact.PickUpItem();
        }
    
    }
}
