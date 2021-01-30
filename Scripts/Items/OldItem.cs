using System.Collections;
using UIComponents;
using UnityEngine;

namespace Items
{
    public class OldItem : MonoBehaviour
    {
        public enum ItemType
        {
            Ingredient,
            Potion,
            Other
        }
    
    
        [SerializeField] private Sprite icon;
        [SerializeField] private ItemType itemType = ItemType.Other;
        [SerializeField] private GameObject model;
        [SerializeField] private AudioClip collectSound;
        public Vector3 modelScale = Vector3.one;
        public float interactRange = 1f;
        public float fadeTime = 0.2f;
        private SphereCollider _collider;
        private GameObject _worldInstance;
    
        // Start is called before the first frame update
        private void Start()
        {
            _collider = gameObject.AddComponent<SphereCollider>();
            _collider.isTrigger = true;
            _collider.radius = interactRange;
            _worldInstance = Instantiate(model, transform);
            _worldInstance.transform.localScale = modelScale;
            var modelCollider = _worldInstance.GetComponent<Collider>();
            if (!modelCollider) return;
            modelCollider.enabled = false;
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        public Sprite GetSprite()
        {
            return icon;
        }

        public ItemType GetItemType()
        {
            return itemType;
        }

        public void AddToInventory()
        {
            var inventoryController = FindObjectOfType<InventoryController>();
            if (!inventoryController) return;
            // inventoryController.PickUpItem(this);
            StartCoroutine(FadeOut(GetComponentInChildren<AudioSource>(), this.gameObject));
            _collider.enabled = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactRange);
        }

        private IEnumerator FadeOut(AudioSource audioSource, GameObject self)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(collectSound);
            Destroy(self, fadeTime);
            yield return null;
        }
    }
}
