using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpookyMurderMystery.Dialogue
{
    [System.Serializable]
    public struct DialogueOption
    {

        public string OptionName;
        public Dialogue DialogueToRenderIfChosen;

    }
}
