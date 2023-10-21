using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpookyMurderMystery.Dialogue
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class WorldInteractor : MonoBehaviour
    {

        public abstract void OnInteract();

    }
}
