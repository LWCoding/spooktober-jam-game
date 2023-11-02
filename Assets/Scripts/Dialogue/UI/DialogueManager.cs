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
        [SerializeField] private List<DialogueOptionHandler> _dialogueOptionHandlers;

        private Queue<DialogueText> _dialogueQueue = new();

        public bool IsAnimating() => _dialogueContainerCanvasGroup.alpha != 0;

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
        public void ShowDialogueUI(float delay = 0.3f)
        {
            _primaryDialogueCharacter.ClearSprite(); // Hide any currently displayed sprite.
            _dialogueSpeakerNameText.text = ""; // Hide any currently displayed name.
            _dialogueText.text = ""; // Hide any currently displayed text.
            StartCoroutine(LerpDialogueUIAlphaCoroutine(0, 1, delay));
        }

        public bool IsDialogueShowing() => _dialogueContainerCanvasGroup.alpha == 1;

        /// <summary>
        /// Hides the dialogue user interface. Takes a while to animate out.
        /// Check IsDialogueShowing() to check when finished.
        /// </summary>
        public void HideDialogueUI(float delay = 0.3f) => StartCoroutine(LerpDialogueUIAlphaCoroutine(1, 0, delay));

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

        private void HideAllDialogueOptions()
        {
            // Disable all dialogue options.
            for (int i = 0; i < _dialogueOptionHandlers.Count; i++)
            {
                _dialogueOptionHandlers[i].gameObject.SetActive(false);
            }
        }

        private IEnumerator RenderDialogueTextCoroutine()
        {
            HideAllDialogueOptions();  // Hide any pre-existing dialogue options!
            // If we're not showing the dialogue UI, make sure that is animated in first.
            if (!IsDialogueShowing())
            {
                ShowDialogueUI();
                yield return new WaitUntil(() => IsDialogueShowing());
            }
            float defTimeBtwnChar = 0.04f;  // Default time to wait between characters.
            float timeElapsed = 0;  // Variable for curr iter, temp variable
            float timeBtwnChar; // Variable used for curr iter, time to wait between characters
            // Loop through all of the current dialogue and render the lines of text.
            while (_dialogueQueue.Count > 0)
            {
                timeBtwnChar = defTimeBtwnChar;
                // Animate first string of dialogue.
                DialogueText dText = _dialogueQueue.Dequeue();
                // Set the speaker sprite if applicable.
                _primaryDialogueCharacter.SetSprite(dText.PrimaryCharacterSprite);
                if (dText.PrimaryCharacterSprite != null)
                {
                    // If the character is new, animate it in.
                    _primaryDialogueCharacter.AnimateCharacterIn();
                }
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
                    // Wait for `timeBtwnChar` seconds. Delay may be skipped by the player.
                    timeElapsed = 0;
                    while (timeElapsed < timeBtwnChar)
                    {
                        timeElapsed += Time.deltaTime;
                        if (Input.GetMouseButtonDown(0))
                        {
                            timeBtwnChar = 0;
                        }
                        yield return null;
                    }
                }
                // Run logic depending on the dialogue's type.
                yield return AfterDialogueRenderedCoroutine(dText);
            }
            // After dialogue is rendered, hide the dialogue box.
            HideDialogueUI();
        }

        private IEnumerator AfterDialogueRenderedCoroutine(DialogueText dText)
        {
            // Run logic depending on the dialogue's type.
            if (dText.HasOptions())
            {
                // Render options. Assume each renders their own logic when selected.
                for (int i = 0; i < _dialogueOptionHandlers.Count; i++)
                {
                    DialogueOptionHandler doh = _dialogueOptionHandlers[i];
                    if (i >= dText.Options.Count)
                    {
                        doh.gameObject.SetActive(false);
                    } else
                    {
                        doh.gameObject.SetActive(true);
                        doh.SetText(dText.Options[i].OptionName);
                        doh.SetDialogue(dText.Options[i].DialogueToRenderIfChosen);
                    }
                }
                StopAllCoroutines();
            }
            else
            {
                // Wait until the player clicks + lifts up the left-mouse button.
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            }
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
