using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpookyMurderMystery
{
    /// <summary>
    /// WorldInteractTrigger is responsible for triggering an event when an object is within its specified radius.
    /// 
    /// To use this class, simply attach the WorldInteractTrigger component to any GameObject,
    /// and select a corresponding function to call in the Unity Inspector.
    /// </summary>
    public class WorldTouchTrigger : WorldTrigger
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsActivateable) { return; }
            _eventToTrigger.Invoke();
        }

    }
}
