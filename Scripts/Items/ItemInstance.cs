using System.Collections;
using UIComponents;
using UnityEngine;

namespace Items
{
    public class ItemInstance : MonoBehaviour, IItemStorage
    {

        [SerializeField] private Item worldItem;
        [SerializeField] private int respawnTime = 60;

        private void Start()
        {
            worldItem.InitializeSelf(gameObject);
        }

        public bool Contains(Item item)
        {
            return (item == worldItem);
        }

        public bool TakeItem(Item item)
        {
            if (item != worldItem) return false;
            this.worldItem = null;
            return true;
        }

        public bool GiveItem(Item item)
        {
            if (worldItem != null) return false;
            worldItem = item;
            return true;
        }

        public bool SpaceAvailable()
        {
            return (worldItem == null);
        }

        public int CountItem(Item item)
        {
            return (worldItem == null) ? 0 : 1;
        }
        
        public void AddToInventory()
        {
            var inventoryController = FindObjectOfType<InventoryController>();
            if (!inventoryController) return;
            inventoryController.PickUpItem(worldItem);
            StartCoroutine(FadeOut(GetComponentInChildren<AudioSource>()));
            GetComponent<Collider>().enabled = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, worldItem.interactRange);
        }

        private IEnumerator FadeOut(AudioSource audioSource)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(worldItem.collectSound);
            Destroy(gameObject, worldItem.fadeTime);
            yield return new WaitForSeconds(respawnTime);
        }
    }
}
