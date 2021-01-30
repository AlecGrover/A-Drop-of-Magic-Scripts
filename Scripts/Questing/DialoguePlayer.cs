using JetBrains.Annotations;
using PlayerComponents;
using TMPro;
using UnityEngine;

namespace Questing
{
    public class DialoguePlayer : MonoBehaviour
    {
        public Dialogue currentDialogue;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI bodyText;
        public DialogueUI dialogueUI;
        private FollowCamera _followCamera;
        private UIController _uiController;
        private IDialoguePath _caller;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void StartDialogue([NotNull] IDialoguePath newCaller, Dialogue dialogue = null)
        {

            Player.movementLock = true;
            
            _caller = newCaller;
            if (dialogue) currentDialogue = dialogue;

            if (!currentDialogue) return;
            
            _followCamera = FindObjectOfType<FollowCamera>();
            _followCamera.enabled = false;
            _uiController = FindObjectOfType<UIController>();
            _uiController.gameObject.SetActive(false);
            dialogueUI.gameObject.SetActive(true);

            if (!(Camera.main is null))
            {
                var transform1 = Camera.main.transform;
                transform1.position = currentDialogue.cameraPosition.position;
                transform1.rotation = currentDialogue.cameraPosition.rotation;
            }
            
            PlayLine();

        }

        private void PlayLine(/*DialogueUI dialogueUI*/)
        {
            var line = currentDialogue.GetNextLine();
            if (line == null)
            {
                GetNextDialogue(/*DialogueUI dialogueUI*/);
                return;
            }

            bodyText.text = line;
            nameText.text = currentDialogue.speakerName;
        }

        private void GetNextDialogue(/*DialogueUI dialogueUI*/)
        {
            currentDialogue = _caller.GetNextDialogue(currentDialogue);
            if (currentDialogue == null)
            {
                dialogueUI.gameObject.SetActive(false);
                _followCamera.enabled = true;
                _followCamera.FixRotation();
                Player.movementLock = false;
                _uiController.gameObject.SetActive(true);
                _caller.OnComplete();
                _caller = null;
                return;
            }
            
            if (!(Camera.main is null))
            {
                var transform1 = Camera.main.transform;
                transform1.position = currentDialogue.cameraPosition.position;
                transform1.rotation = currentDialogue.cameraPosition.rotation;
            }

            PlayLine( /*DialogueUI dialogueUI*/);
        }

        public void OnClick(/*DialogueUI dialogueUI*/)
        {
            PlayLine(/*DialogueUI dialogueUI*/);
        }
        
        
    }
}
