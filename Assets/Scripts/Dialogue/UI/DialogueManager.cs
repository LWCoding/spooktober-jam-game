using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpookyMurderMystery.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {

        public static DialogueManager Instance;
        [Header("Object Assignments")]
        [SerializeField] private CanvasGroup _dialogueContainerCanvasGroup;
        [SerializeField] private DialogueCharacter _primaryDialogueCharacter;
        [SerializeField] private TextMeshProUGUI _dialogueSpeakerNameText;
        [SerializeField] private TextMeshProUGUI _dialogueText;

        private Queue<DialogueText> _dialogueQueue = new();

        private void Awake()
        {
            // If there is already an instance, destroy this.
            if (Instance != null) Destroy(gameObject);
            Instance = this;
        }

        private void Start()
        {
            HideDialogueUI(0); // Initially ensure dialogue is hidden.
        }

        /// <summary>
        /// Shows the dialogue user interface. Takes a while to animate in.
        /// Check IsDialogueShowing() to check when finished.
        /// </summary>
        public void ShowDialogueUI(float delay = 0.8f)
        {
            _primaryDialogueCharacter.ClearSprite(); // Hide any currently displayed sprite.
            _dialogueSpeakerNameText.text = ""; // Hide any currently displayed name.
            _dialogueText.text = ""; // Hide any currently displayed text.
            StartCoroutine(LerpDialogueUIAlphaCoroutine(0, 1, delay));
        }

        /// <summary>
        /// Hides the dialogue user interface. Takes a while to animate out.
        /// Check IsDialogueShowing() to check when finished.
        /// </summary>
        public void HideDialogueUI(float delay = 0.8f) => StartCoroutine(LerpDialogueUIAlphaCoroutine(1, 0, delay));

        /// <summary>
        /// Removes all instances of dialogue from the current queue.
        /// </summary>  
        public void ClearDialogueQueue() => _dialogueQueue = new Queue<DialogueText>();

        /// <summary>
        /// Queues strings to be rendered as dialogue. Does NOT start
        /// actually start animating dialogue.
        /// </summary>  
        public void QueueDialogueText(DialogueText[] texts)
        {
            foreach (DialogueText text in texts)
            {
                _dialogueQueue.Enqueue(text);
            }
        }

        /// <summary>
        /// Alternative way to call QueueDialogueText given Dialogue object.
        /// </summary>
        public void QueueDialogueText(Dialogue d) => QueueDialogueText(d.DialogueTexts);

        /// <summary>
        /// Renders dialogue currently in QueueDialogueText as an animation.
        /// </summary>
        public void RenderDialogueText()
        {
            StopAllCoroutines();
            StartCoroutine(RenderDialogueTextCoroutine());
        }

        private bool IsDialogueShowing() => _dialogueContainerCanvasGroup.alpha == 1;

        private IEnumerator RenderDialogueTextCoroutine()
        {
            // If we're not showing the dialogue UI, make sure that is animated in first.
            if (!IsDialogueShowing())
            {
                ShowDialogueUI();
                yield return new WaitUntil(() => IsDialogueShowing());
            }
            // Loop through all of the current dialogue and render the lines of text.
            while (_dialogueQueue.Count > 0)
            {
                // Animate first string of dialogue.
                DialogueText dText = _dialogueQueue.Dequeue();
                // Set the speaker sprite if applicable.
                _primaryDialogueCharacter.SetSprite(dText.PrimaryCharacterSprite);
                // Set the speaker name.
                _dialogueSpeakerNameText.text = dText.SpeakerName;
                // Set the destination text and create an empty string builder.
                string destinationStr = dText.Text;
                StringBuilder currentStrBldr = new StringBuilder("", destinationStr.Length);
                // Repeat until the string builder becomes the destination text.
                while (currentStrBldr.Length != destinationStr.Length)
                {
                    currentStrBldr.Append(destinationStr[currentStrBldr.Length]);
                    _dialogueText.text = currentStrBldr.ToString();
                    yield return new WaitForSeconds(0.05f);
                }
                // Wait.
                yield return new WaitForSeconds(1);
            }
            // After dialogue is rendered, hide the dialogue box.
            HideDialogueUI();
        }

        private IEnumerator LerpDialogueUIAlphaCoroutine(float initialAlpha, float targetAlpha, float timeToWait)
        {
            float currTime = 0;
            while (currTime <= timeToWait)
            {
                currTime += Time.deltaTime;
                _dialogueContainerCanvasGroup.alpha = Mathf.Lerp(initialAlpha, targetAlpha, currTime / timeToWait);
                yield return null;
            }
        }

    }
}