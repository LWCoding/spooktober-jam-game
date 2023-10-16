using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpookyMurderMystery.Dialogue
{
    [RequireComponent(typeof(Image))]
    public class DialogueCharacter : MonoBehaviour
    {

        private Image _characterImage;
        private Vector2 _initialPosition;

        private void Awake()
        {
            _characterImage = GetComponent<Image>();
            _initialPosition = transform.position;
        }

        /// <summary>
        /// Animates character sprite into the screen.
        /// Recommended for new characters to show they're joining the convo.<br/>
        /// Takes a while to animate. Use IsCharacterShowing() to check if finished.
        /// </summary>
        public void AnimateCharacterIn()
        {
            StartCoroutine(AnimateCharacterInCoroutine());
        }

        /// <summary>
        /// Checks if character has fully shown. May be False if the character is being
        /// animated in or is hidden.
        /// </summary>
        /// <returns>A boolean representing if the character is fully opaque.</returns>
        public bool IsCharacterShowing() => _characterImage.color.a == 1;

        /// <summary>
        /// Immediately sets this dialogue character's sprite.
        /// If null, does nothing.
        /// </summary>
        public void SetSprite(Sprite sprite)
        {
            if (sprite == null) return;
            _characterImage.enabled = true;
            _characterImage.sprite = sprite;
        }

        /// <summary>
        /// Sets the dialogue character's sprite to be invisible.
        /// </summary>
        public void ClearSprite() => _characterImage.enabled = false;

        private IEnumerator AnimateCharacterInCoroutine()
        {
            float currTime = 0;
            float timeToWait = 0.3f;
            Vector2 modifiedPosition = _initialPosition + new Vector2(-100, 0);
            Color initialColor = Color.white - new Color(0, 0, 0, 1);
            Color targetColor = Color.white;
            while (currTime < timeToWait)
            {
                currTime += Time.deltaTime;
                transform.position = Vector2.Lerp(modifiedPosition, _initialPosition, Mathf.SmoothStep(0, 1, currTime / timeToWait));
                _characterImage.color = Color.Lerp(initialColor, targetColor, currTime / timeToWait);
                yield return null;
            }
        }

    }
}
