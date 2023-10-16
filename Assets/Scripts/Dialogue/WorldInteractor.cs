using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpookyMurderMystery.Dialogue
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class WorldInteractor : MonoBehaviour
    {

        // When this sprite is clicked, show dialogue if not 
        // already happening.
        private void OnMouseDown()
        {
            OnInteract();
        }

        public abstract void OnInteract();

    }
}
