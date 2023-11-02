using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpookyMurderMystery
{
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
                _eventToTrigger.Invoke(this);
            }
        }

    }
}
