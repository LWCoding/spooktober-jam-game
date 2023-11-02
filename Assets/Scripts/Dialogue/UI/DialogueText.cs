using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpookyMurderMystery.Dialogue
{
    [System.Serializable]
    public struct DialogueText
    {

        [Header("Text Information")]
        public string SpeakerName;
        public string Text;
        [Header("Sprite Information")]
        [Tooltip("The sprite the character should become as it says this dialogue. If None, sprite will be unchanged.")]
        public Sprite PrimaryCharacterSprite;

        public DialogueText(string name, string text) : this()
        {
            SpeakerName = name;
            Text = text;
        }

    }
}
