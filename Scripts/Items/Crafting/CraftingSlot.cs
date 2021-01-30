using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UIComponents;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Items.Crafting
{
    public class CraftingSlot : MonoBehaviour, IItemStorage, IPointerDownHandler
    {

        public Item heldItem;
        public bool holding = false;
        private bool _thisFrame = false;
        private GameObject _iconSlot;
        private Image _icon;
        private readonly Color _iconColorWhite = new Color(255, 255, 255, 255);
        private readonly Color _iconColorClear = new Color(255, 255, 255, 0);
        private Vector3 _defaultPosition;

        private void Awake()
        {
            _iconSlot = new GameObject();
            _iconSlot.transform.parent = transform;
            _icon = _iconSlot.AddComponent<Image>();
            _icon.color = _iconColorClear;
            _defaultPosition = _icon.transform.position;
        }

        private void Start()
        {
            _iconSlot.transform.localPosition = Vector3.zero;
            _iconSlot.transform.localScale = Vector3.one;
        }
        
        private void Update()
        {
            if (!holding) return;
            DeleteItem.active = true;
            var pointer = Mouse.current;
            _iconSlot.transform.position = pointer.position.ReadValue();
            if (!Mouse.current.leftButton.wasPressedThisFrame) return;
            if (_thisFrame)
            {
                _thisFrame = false;
                return;
            }
            RaycastDrop();
        }
        
        public bool Contains(Item item)
        {
            return (this.heldItem == item);
        }

        public bool TakeItem(Item item)
        {
            if (this.heldItem != item) return false;
            _icon.sprite = null;
            _icon.color = _iconColorClear;
            this.heldItem = null;
            return true;
        }

        public bool DropItem()
        {
            if (!heldItem) return false;
            _icon.sprite = null;
            _icon.color = _iconColorClear;
            heldItem = null;
            return true;
        }

        public bool GiveItem(Item item)
        {
            if (this.heldItem) return false;
            this.heldItem = item;
            _icon.sprite = item.GetSprite();
            _icon.color = _iconColorWhite;
            return true;
        }

        public bool SpaceAvailable()
        {
            return (!heldItem);
        }

        public int CountItem(Item item)
        {
            return (this.heldItem == item) ? 1 : 0;
        }

        private void RaycastDrop()
        {
            var over = new List<RaycastResult>();
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = Mouse.current.position.ReadValue()
            };
            EventSystem.current.RaycastAll(eventData, over);
            foreach (var raycast in over.ToList())
            {
                // Debug.Log(raycast.gameObject.name);
                var deleteItem = raycast.gameObject.GetComponent<DeleteItem>();
                if (deleteItem)
                {
                    StartCoroutine(CancelHolding());
                    DropItem();
                    return;
                }
                var inventorySlot = raycast.gameObject.GetComponent<InventorySlot>();
                if (inventorySlot)
                {
                    if (inventorySlot.SpaceAvailable())
                    {
                        inventorySlot.GiveItem(heldItem);
                        StartCoroutine(CancelHolding());
                        DropItem();
                        return;
                    }
                    var newItem = inventorySlot.heldItem;
                    inventorySlot.CancelCoroutine();
                    inventorySlot.DropItem();
                    inventorySlot.GiveItem(heldItem);
                    DropItem();
                    GiveItem(newItem);
                    return;
                }
                var craftingSlot = raycast.gameObject.GetComponent<CraftingSlot>();
                if (!craftingSlot) continue;
                {
                    if (craftingSlot == this)
                    {
                        StartCoroutine(CancelHolding());
                        return;
                    }
                    if (craftingSlot.SpaceAvailable())
                    {
                        craftingSlot.GiveItem(heldItem);
                        StartCoroutine(CancelHolding());
                        DropItem();
                        return;
                    }

                    var newItem = craftingSlot.heldItem;
                    craftingSlot.CancelCoroutine();
                    craftingSlot.DropItem();
                    craftingSlot.GiveItem(heldItem);
                    DropItem();
                    GiveItem(newItem);
                    return;
                }
            }
            StartCoroutine(CancelHolding());
        }

        public void CancelCoroutine()
        {
            StartCoroutine(CancelHolding());
        }
        
        private IEnumerator CancelHolding()
        {
            yield return new WaitForEndOfFrame();
            holding = false;
            _iconSlot.transform.SetParent(transform);
            _iconSlot.transform.localPosition = Vector3.zero;
            DeleteItem.active = false;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (holding) return;
            if (SpaceAvailable()) return;
            holding = true;
            _thisFrame = true;
            _iconSlot.transform.SetParent(transform.parent.parent.parent);
            _iconSlot.transform.SetAsLastSibling();
        }
    }
}
