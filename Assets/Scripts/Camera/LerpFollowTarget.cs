using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpookyMurderMystery
{
    public class LerpFollowTarget : MonoBehaviour
    {

        [Header("Target Assignment")]
        [SerializeField] private Transform _transformToFollow;
        [Tooltip("Distance away until camera stops trying to lerp towards target.")]
        [SerializeField] private float _maxFollowDistance;

        private void Awake()
        {
            Debug.Assert(_transformToFollow != null, "Transform for object to follow was not assigned!", this);
        }

        private void FixedUpdate()
        {
            if (Vector2.Distance(transform.position, _transformToFollow.position) > _maxFollowDistance)
            {
                Vector3 newPos = Vector2.Lerp(transform.position, _transformToFollow.position, 0.1f);
                newPos.z = -10;
                transform.position = newPos;
            }
        }

    }
}
