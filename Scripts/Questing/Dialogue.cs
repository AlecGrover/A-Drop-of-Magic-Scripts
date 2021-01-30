using System.Collections.Generic;
using UnityEngine;

namespace Questing
{
    [CreateAssetMenu]
    public class Dialogue : ScriptableObject
    {
        public Transform cameraPosition;
        public List<string> lines;
        public Dialogue nextDialogue;
        public string speakerName;

        public DialogueFork fork;

        private int _currentLine = 0;

        public Dialogue GetNextDialogue()
        {
            _currentLine = 0;
            return fork ? fork.GetNextDialogue() : nextDialogue;
        }

        public Dialogue GetNextDialogue(DialogueTrigger trigger)
        {
            _currentLine = 0;
            return !fork ? nextDialogue : fork.GetNextDialogue(trigger);
        }
        
        public string GetNextLine()
        {
            if (_currentLine >= lines.Count) return null;
            _currentLine++;
            return lines[_currentLine - 1];

        }

    }
}
