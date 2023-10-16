using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpookyMurderMystery.Dialogue
{

    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject
    {

        public DialogueText[] DialogueTexts;

    }

}
