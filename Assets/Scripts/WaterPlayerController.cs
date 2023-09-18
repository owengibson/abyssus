using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class WaterPlayerController : MonoBehaviour
    {
        [SerializeField] private float _playerSpeed = 5f;

        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void MovePlayer()
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var verticalInput = Input.GetAxisRaw("Vertical");
            //_rigidbody2D.velocity = new Vector2(horizontalInput, verticalInput) * _playerSpeed;
            _rigidbody2D.AddForce(new Vector2(horizontalInput, verticalInput) * _playerSpeed);
        }

        /*private void Update()
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                MovePlayer();
            }
        }*/

        private void FixedUpdate()
        {
            MovePlayer();
        }
    }
}
