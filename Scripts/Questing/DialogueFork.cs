using Items;
using PlayerComponents;
using UIComponents;
using UnityEngine;

namespace Questing
{
    [CreateAssetMenu]
    public class DialogueFork : ScriptableObject
    {

        public Item requiredItem;
        public bool consumeItem = true;
        public Dialogue missingItemDialogue;
        public Dialogue hasItemDialogue;

        public DialogueTrigger[] toggleTriggersOnSuccess;

        public Dialogue GetNextDialogue(DialogueTrigger trigger = null)
        {
            var player = FindObjectOfType<Player>();
            var inventoryController = player.inventoryController;
            if (consumeItem)
            {
                if (!inventoryController.TakeItem(requiredItem)) return missingItemDialogue;
            }
            else
            {
                if (!inventoryController.Contains(requiredItem)) return missingItemDialogue;
            }
            if (trigger) trigger.PassedCheck();
            return hasItemDialogue;
        }
    }
}
