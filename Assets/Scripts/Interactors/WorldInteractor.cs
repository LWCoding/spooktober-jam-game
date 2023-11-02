using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpookyMurderMystery.Dialogue
{
    /// <summary>
    /// Base class for handling specific interactions in the game world, designed to be used in conjunction with the WorldTrigger class.
    /// It includes an 'IsBusy' flag to prevent simultaneous interaction, such as rendering new dialogue while animation is in progress.
    /// 
    /// Note: This class should not be instantiated or copied directly.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public abstract class WorldInteractor : MonoBehaviour
    {

        public bool IsBusy = false;

        public abstract void OnInteract();
        public abstract IEnumerator OnCompleteCoroutine();

    }
}
