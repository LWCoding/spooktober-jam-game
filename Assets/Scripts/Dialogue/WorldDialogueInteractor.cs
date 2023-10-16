using SpookyMurderMystery.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpookyMurderMystery.Dialogue
{
    public class WorldDialogueInteractor : WorldInteractor
    {

        [Header("Dialogue Assignment")]
        public Dialogue DialogueToRender;

        private DialogueManager _dialogueManager;

        private void Start()
        {
            _dialogueManager = DialogueManager.Instance;
        }

        public override void OnInteract()
        {
            _dialogueManager.ClearDialogueQueue();
            _dialogueManager.QueueDialogueText(DialogueToRender);
            _dialogueManager.RenderDialogueText();
        }

    }
}
