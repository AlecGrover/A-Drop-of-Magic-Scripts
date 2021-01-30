using System.Collections;
using System.Collections.Generic;
using Items;
using UIComponents;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{

    public Item item;
    private bool _holding = false;
    private InventoryController _inventoryController;

    // Start is called before the first frame update
    void Start()
    {
        _inventoryController = FindObjectOfType<InventoryController>();
        StartCoroutine(PickUpAndDrop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator PickUpAndDrop()
    {
        if (!_inventoryController) yield return null;
        while (true)
        {
            yield return new WaitForSeconds(3);
            _holding = !_holding;
            var handleItem = _holding ? _inventoryController.PickUpItem(item) : _inventoryController.DropItemBySlot(0);
        }
    }
    
}
