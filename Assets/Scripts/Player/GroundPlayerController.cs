using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class GroundPlayerController : MonoBehaviour
    {
        [SerializeField] private float _playerSpeed = 5.0f;
        [SerializeField] private float _jumpPower = 5.0f;
        [SerializeField] private LayerMask _groundLayerMask;

        private Rigidbody2D _rigidbody2D;
        private CapsuleCollider2D _capsuleCollider2D;
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }
        private void Update()
        {
            if (Input.GetButton("Jump") && IsGrounded())
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            _rigidbody2D.velocity = new Vector2(horizontalInput * _playerSpeed, _rigidbody2D.velocity.y);
        }
        private void Jump() => _rigidbody2D.velocity = new Vector2(0, _jumpPower);

        private bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(_capsuleCollider2D.bounds.center, Vector2.down, _capsuleCollider2D.bounds.extents.y + 0.02f, _groundLayerMask);
            return hit.collider != null;
        }
    }
}
