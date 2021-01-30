using System;
using System.Collections;
using UIComponents;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    [Serializable]
    public enum ItemType
    {
        Ingredient,
        Potion,
        Other
    }
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {

        public Sprite icon;
        public ItemType itemType = ItemType.Other;
        public GameObject model;
        public AudioClip collectSound;
        public Vector3 modelScale = Vector3.one;
        public float interactRange = 1f;
        public float fadeTime = 0.2f;
        [FormerlySerializedAs("_collider")] public SphereCollider collider;
        private GameObject _worldInstance;

        public void InitializeSelf(GameObject self)
        {
            collider = self.AddComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = interactRange;
            _worldInstance = Instantiate(model, self.transform);
            _worldInstance.transform.localScale = modelScale;
            var modelCollider = _worldInstance.GetComponent<Collider>();
            if (!modelCollider) return;
            modelCollider.enabled = false;
        }

        public Sprite GetSprite()
        {
            return icon;
        }

        public ItemType GetItemType()
        {
            return itemType;
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
