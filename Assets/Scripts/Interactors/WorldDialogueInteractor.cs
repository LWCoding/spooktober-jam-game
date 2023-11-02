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

        public override void OnInteract(WorldTrigger activatedTrigger)
        {
            activatedTrigger.IsActivateable = false;
            _dialogueManager.ClearDialogueQueue();
            _dialogueManager.QueueDialogueText(DialogueToRender);
            _dialogueManager.RenderDialogueText();
            StartCoroutine(OnCompleteCoroutine(activatedTrigger));
        }

        public override IEnumerator OnCompleteCoroutine(WorldTrigger activatedTrigger)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => !DialogueManager.Instance.IsAnimating());
            activatedTrigger.IsActivateable = true;
        }

    }
}
