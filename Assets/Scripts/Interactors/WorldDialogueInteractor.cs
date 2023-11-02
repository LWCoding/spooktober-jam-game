using SpookyMurderMystery.Dialogue;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
namespace SpookyMurderMystery.Dialogue
{
    /// <summary>
    /// WorldDialogueInteractor controls interactions that render dialogue when interacted with in world space.
    /// The Dialogue must be supplied as a ScriptableObject to this component.
    /// 
    /// To use this class, attach the WorldDialogueInteractor component to an object and trigger it using an object with any WorldTrigger script.
    /// </summary>
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
            if (IsBusy) return;  // If this interactor is already doing something, stop.
            _dialogueManager.ClearDialogueQueue();
            _dialogueManager.QueueDialogueText(DialogueToRender);
            _dialogueManager.RenderDialogueText();
            StartCoroutine(OnCompleteCoroutine());
        }

        public override IEnumerator OnCompleteCoroutine()
        {
            IsBusy = true;
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => !_dialogueManager.IsAnimating());
            IsBusy = false;
        }

    }
}
