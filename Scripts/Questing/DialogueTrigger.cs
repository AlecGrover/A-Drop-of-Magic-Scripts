using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlayerComponents;
using UnityEngine;
using UnityEngine.Serialization;

namespace Questing
{
    public class DialogueTrigger : MonoBehaviour, IDialoguePath
    {

        public Dialogue firstDialogue;
        public DialoguePlayer dialoguePlayer;
        public List<DialogueTrigger> toggleNextDialogueTriggers;
        public List<DialogueTrigger> toggleTriggersOnSuccess;
        public bool stayActive = false;
        public bool repositionPlayer = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                StartFirstDialogue();
            }
        }

        public void StartFirstDialogue()
        {
            dialoguePlayer.StartDialogue(this, firstDialogue);
        }

        public Dialogue GetNextDialogue(Dialogue dialogue)
        {
            return (dialogue.GetNextDialogue(this));
        }

        public void OnComplete()
        {

            if (repositionPlayer)
            {
                var player = FindObjectOfType<Player>();
                //if (player) player.gameObject.transform.position += (transform.Find("PlayerLocation").position);
                var position = transform.GetChild(0).position;
                if (player) player.SetLocation(new Vector3(position.x, player.transform.position.y, position.z));
            }
            
            StartCoroutine(ToggleTriggers());
            
        }

        private IEnumerator ToggleTriggers()
        {
            yield return new WaitForSeconds(2 * Time.deltaTime);
            foreach (var dialogueTrigger in toggleNextDialogueTriggers)
            {
                dialogueTrigger.gameObject.SetActive(!dialogueTrigger.gameObject.activeSelf);
            }
            if (!stayActive)
            {
                gameObject.SetActive(false);
            }
        }

        public void PassedCheck()
        {
            foreach (var trigger in toggleTriggersOnSuccess) toggleNextDialogueTriggers.Add(trigger);
        }
    }
}
