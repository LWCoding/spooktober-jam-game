using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpookyMurderMystery
{
    public class WorldTouchTrigger : WorldTrigger
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _eventToTrigger.Invoke();
        }

    }
}