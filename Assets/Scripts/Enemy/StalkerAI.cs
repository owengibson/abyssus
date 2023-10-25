using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class StalkerAI : MonoBehaviour
    {
        [SerializeField] private float _chaseSpeed;

        private Transform _player;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _player = PlayerSpawner.Instance.Player.transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();

            InvokeRepeating("UpdateTarget", 0.5f, 0.5f);
        }

        private void UpdateTarget()
        {
            _rigidbody2D.DOKill();
            _rigidbody2D.DOMove(_player.position, _chaseSpeed).SetSpeedBased(true).SetEase(Ease.Linear);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            collision.GetComponent<IDamageable>().TakeDamage(1000);
        }
    }
}
