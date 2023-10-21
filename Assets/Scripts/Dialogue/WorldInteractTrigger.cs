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

        private bool _isActivatable = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _popupObject.SetActive(true);
            _isActivatable = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _popupObject.SetActive(false);
            _isActivatable = false;
        }

        private void Update()
        {
            if (!_isActivatable) { return; }
            if (Input.GetKeyDown(KeyCode.E))
            {
                _eventToTrigger.Invoke();
            }
        }

    }
}
