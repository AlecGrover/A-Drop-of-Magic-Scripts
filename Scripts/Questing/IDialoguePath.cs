using UnityEngine;

namespace Questing
{
    public interface IDialoguePath
    {
        void StartFirstDialogue();
        Dialogue GetNextDialogue(Dialogue dialogue);
        void OnComplete();
    }
}
