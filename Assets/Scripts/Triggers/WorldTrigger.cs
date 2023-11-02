using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpookyMurderMystery
{
    /// <summary>
    /// WorldTrigger is responsible for triggering specific interactions and is designed to work alongside
    /// the WorldInteractor class to initiate particular events.
    /// 
    /// Includes an 'IsActivateable' flag that ensures this class can only be invoked when specific criteria are met,
    /// such as when the player is in range and presses the 'E' key.
    /// 
    /// Note: This class should not be instantiated or copied directly.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public abstract class WorldTrigger : MonoBehaviour
    {

        [Header("Event to Trigger")]
        [SerializeField] protected UnityEvent _eventToTrigger;

        public bool IsActivateable = false;

        private void Awake()
        {
            Debug.Assert(GetComponent<Collider2D>().isTrigger, "Collider on trigger must have trigger property set to true!", this);
        }

    }
}
