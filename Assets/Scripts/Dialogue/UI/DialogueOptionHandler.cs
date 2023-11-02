using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SpookyMurderMystery.Dialogue
{
    public class DialogueOptionHandler : MonoBehaviour, IPointerClickHandler
    {

        [Header("Object Assignments")]
        [SerializeField] private TextMeshProUGUI _optionText;

        private Dialogue _dialogueToRender;

        /// <summary>
        /// Sets the text of the option.
        /// </summary>
        /// <param name="text">The string to set the option text to</param>
        public void SetText(string text)
        {
            _optionText.text = text;
        }

        /// <summary>
        /// Sets the dialogue to play when this option is interacted with.
        /// </summary>
        /// <param name="d">Dialogue to render when interacted</param>
        public void SetDialogue(Dialogue d)
        {
            _dialogueToRender = d;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Assert(_dialogueToRender != null, "Dialogue to render when option is clicked is null!", this);
            // Render the new dialogue when this is clicked.
            DialogueManager.Instance.ClearDialogueQueue();
            DialogueManager.Instance.QueueDialogueText(_dialogueToRender);
            DialogueManager.Instance.RenderDialogueText();
        }
    }
}
