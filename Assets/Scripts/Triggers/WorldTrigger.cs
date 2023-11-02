using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpookyMurderMystery
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class WorldTrigger : MonoBehaviour
    {

        [Header("Event to Trigger")]
        [SerializeField] protected UnityEvent<WorldTrigger> _eventToTrigger;

        public bool IsActivateable = false;

        private void Awake()
        {
            Debug.Assert(GetComponent<Collider2D>().isTrigger, "Collider on trigger must have trigger property set to true!", this);
        }

    }
}
