using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpookyMurderMystery
{
    /// <summary>
    /// WorldInteractTrigger is responsible for triggering an event when an object is within the specified radius
    /// of this object, and the user presses the "E" key (the interact button).
    /// 
    /// To use this class, simply attach the WorldInteractTrigger component to any GameObject
    /// and select a corresponding function to call in the Unity Inspector.
    /// </summary>
    public class WorldInteractTrigger : WorldTrigger
    {

        [Header("Object Assignments")]
        [Tooltip("The object that will show up when this object is in radius")]
        [SerializeField] private GameObject _popupObject;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IsActivateable = true;
            _popupObject.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            IsActivateable = false;
            _popupObject.SetActive(false);
        }

        private void Update()
        {
            if (!IsActivateable) { return; }
            if (Input.GetKeyDown(KeyCode.E))
            {
                _eventToTrigger.Invoke();
            }
        }

    }
}
