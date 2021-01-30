using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using Items.Crafting;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UIComponents
{
    public class InventorySlot : MonoBehaviour, IItemStorage, IPointerDownHandler
    {

        public bool holding = false;
        private bool _thisFrame = false;
        public Item heldItem;
        private GameObject _iconSlot;
        private Image _icon;
        private readonly Color _iconColorWhite = new Color(255, 255, 255, 255);
        private readonly Color _iconColorClear = new Color(255, 255, 255, 0);
        private Vector3 _defaultPosition;

        private void Awake()
        {
            _iconSlot = new GameObject();
            _iconSlot.transform.SetParent(transform);
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
                    if (inventorySlot == this)
                    {
                        StartCoroutine(CancelHolding());
                        return;
                    }
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

        public Item GetHeldItem()
        {
            return heldItem;
        }

        public Item RemoveHeldItem()
        {
            var returnItem = heldItem;
        
            _icon.sprite = null;
            _icon.color = _iconColorClear;
            heldItem = null;
            return returnItem;
        }

        public void RemoveItem()
        {
            _icon.sprite = null;
            _icon.color = _iconColorClear;
            heldItem = null;
        }

        public bool AddHeldItem(Item item)
        {
            if (heldItem) return false;
        
            _icon.sprite = item.GetSprite();
            _icon.color = _iconColorWhite;
        
            heldItem = item;
            return true;
        }


        public bool Contains([NotNull] Item item)
        {
            return item == heldItem;
        }

        public bool TakeItem([NotNull] Item item)
        {
            if (item != heldItem) return false;
            _icon.sprite = null;
            _icon.color = _iconColorClear;
            heldItem = null;
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
            if (heldItem) return false;
            _icon.sprite = item.GetSprite();
            _icon.color = _iconColorWhite;
            heldItem = item;
            return true;
        }

        public bool SpaceAvailable()
        {
            return (!heldItem);
        }

        public int CountItem(Item item)
        {
            return heldItem == item ? 1 : 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (holding) return;
            if (SpaceAvailable()) return;
            holding = true;
            _thisFrame = true;
            _iconSlot.transform.SetParent(transform.parent);
            _iconSlot.transform.SetAsLastSibling();
        }

    }
}
