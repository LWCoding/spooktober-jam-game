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
        public Sprite PrimaryCharacterSprite;

        public DialogueText(string name, string text) : this()
        {
            SpeakerName = name;
            Text = text;
        }

    }
}
