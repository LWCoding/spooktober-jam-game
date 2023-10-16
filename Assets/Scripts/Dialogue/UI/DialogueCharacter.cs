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

        private void Awake()
        {
            _characterImage = GetComponent<Image>();
        }

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

    }
}
