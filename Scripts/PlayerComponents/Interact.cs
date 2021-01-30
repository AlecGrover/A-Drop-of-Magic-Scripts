using System.Collections;
using System.Collections.Generic;
using Items;
using PlayerComponents;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    
    private GameObject _openable;
    private GameObject _collectible;
    [SerializeField] private GameObject body;
    private Animator _animator;
    private bool _inAnimation = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = body.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (!_openable && other.tag.Equals("Openable"))
        {
            _openable = other.gameObject;
        }
        else if (!_collectible && other.tag.Equals("Collectible"))
        {
            _collectible = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _openable)
        {
            _openable = null;
        }
        else if (other.gameObject == _collectible)
        {
            _collectible = null;
        }
    }
    
    public void OnOpen(InputAction.CallbackContext context)
    {
        if (_openable)
        {
            if (_inAnimation)
            {
                return;
            }
            _inAnimation = true;
            Player.movementLock = true;
            Interactable openable = _openable.GetComponent<Interactable>();
            if (openable.GetOpen())
            {
                _animator.SetTrigger("CloseChest");
            }
            else
            {
                _animator.SetTrigger("OpenChest");
            }
            //TriggerInteract();
        }
        else if (_collectible)
        {
            if (_inAnimation) return;
            Player.movementLock = true;
            _inAnimation = true;
            _animator.SetTrigger("PickUp");

        }
    }

    public void TriggerInteract()
    {
        _openable.GetComponent<Interactable>().Interact();
    }

    public void EndInteraction()
    {
        _inAnimation = false;
        Player.movementLock = false;
    }

    public void PickUpItem()
    {
        _collectible.GetComponent<ItemInstance>().AddToInventory();
    }

    public bool GetInAnimation()
    {
        return _inAnimation;
    }
    
}
