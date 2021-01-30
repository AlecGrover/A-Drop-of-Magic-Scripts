using System.Collections;
using System.Collections.Generic;
using Items;
using PlayerComponents;
using UIComponents;
using UnityEngine;
using UnityEngine.Serialization;

public class Interactable : MonoBehaviour, IItemStorage
{

    [SerializeField] private string interactTriggerName;
    private Animator _animator;
    private MonoBehaviour _behaviour;
    private bool _open = false;
    [SerializeField] private AudioSource audioSource;
    public Item heldItem;
    
    // Start is called before the first frame update
    void Start()
    {
        _behaviour = null;
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (interactTriggerName == null)
        {
            return;
        }
        if (audioSource)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
        _open = !_open;
        if (!SpaceAvailable())
        {
            var player = FindObjectOfType<Player>();
            if (player.inventoryController.SpaceAvailable())
            {
                player.inventoryController.GiveItem(heldItem);
                DropItem();
            }
        }
        _animator.SetTrigger(interactTriggerName);
        if (_behaviour)
        {
            
        }
    }

    public bool GetOpen()
    {
        return _open;
    }

    public bool Contains(Item item)
    {
        return (heldItem == item);
    }

    public bool TakeItem(Item item)
    {
        if (!heldItem || heldItem != item) return false;
        heldItem = null;
        return true;
    }

    private bool DropItem()
    {
        if (!heldItem) return false;
        heldItem = null;
        return true;
    }

    public bool GiveItem(Item item)
    {
        if (heldItem) return false;
        heldItem = item;
        return true;
    }

    public bool SpaceAvailable()
    {
        return (!heldItem);
    }

    public int CountItem(Item item)
    {
        return (heldItem == item) ? 1 : 0;
    }
}
