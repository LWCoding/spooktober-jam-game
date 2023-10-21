using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpookyMurderMystery
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementHandler : MonoBehaviour
    {

        [Header("Player Properties")]
        [SerializeField] private float _moveSpeed;

        private Rigidbody2D _rb2D;

        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _rb2D.velocity = new Vector2(Input.GetAxis("Horizontal") * _moveSpeed,
                                         Input.GetAxis("Vertical") * _moveSpeed);
        }

    }
}
