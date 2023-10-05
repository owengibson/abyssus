using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CaveGame
{
    public class EnemyController : MonoBehaviour, IDamageable, IPointerClickHandler
    {
        public enum PatrolDirection { Left, Right }

        public EnemyState CurrentState = EnemyState.Patrol;

        [SerializeField] private EnemySO _enemy;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Slider _healthBar;
        [Space]

        [SerializeField] private float _detectionRadius = 15f;
        [SerializeField] private float _patrolCycleLength = 4f;
        [SerializeField] private float _chaseSpeed = 2f;

        private float _currentHealth;

        private PatrolDirection _patrolDirection = PatrolDirection.Right;
        private Vector3 _spawnPosition;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _currentHealth = _enemy.MaxHealth;
            _spawnPosition = transform.position;

            Invoke("Patrol", Random.Range(0, 2));
        }

        private void Update()
        {
            /*if (_rigidbody2D.velocity.x >= 0)
            {
                _spriteRenderer.flipX = false;
            }
            else
            {
                _spriteRenderer.flipX = true;
            }*/

            if (CurrentState!= EnemyState.Chase && Vector2.Distance(transform.position, PlayerSpawner.Instance.Player.transform.position) <= _detectionRadius)
            {
                Chase();
            }
            if (CurrentState == EnemyState.Chase && Vector2.Distance(transform.position, PlayerSpawner.Instance.Player.transform.position) > _detectionRadius)
            {
                Patrol();
            }
        }

        #region PATROL
        private void Patrol()
        {
            CurrentState = EnemyState.Patrol;

            if (transform.position != _spawnPosition)
            {
                _rigidbody2D.DOMove(transform.position, 10).SetSpeedBased(true);
            }
            else
            {
                PatrolMoveRight();
            }
        }

        private void PatrolMoveRight()
        {
            _rigidbody2D.DOKill();
            _patrolDirection = PatrolDirection.Right;
            _spriteRenderer.flipX = false;
            _rigidbody2D.DOMoveX(_spawnPosition.x + _enemy.PatrolDistance, _patrolCycleLength).SetEase(Ease.InOutSine).OnComplete(PatrolMoveLeft);
        }

        private void PatrolMoveLeft()
        {
            _rigidbody2D.DOKill();
            _patrolDirection = PatrolDirection.Left;
            _spriteRenderer.flipX = true;
            _rigidbody2D.DOMoveX(_spawnPosition.x - _enemy.PatrolDistance, _patrolCycleLength).SetEase(Ease.InOutSine).OnComplete(PatrolMoveRight);
        }
        #endregion

        private void Chase()
        {
            _rigidbody2D.DOKill();
            CurrentState = EnemyState.Chase;
        }

        private void Attack(IDamageable target)
        {
            _rigidbody2D.DOKill();
            CurrentState = EnemyState.Attack;

            target.TakeDamage(_enemy.Damage);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Change patrol direction when bumping into walls
            if (collision.gameObject.CompareTag("Ground"))
            {
                if (_patrolDirection == PatrolDirection.Left) PatrolMoveRight();
                else PatrolMoveLeft();
            }

            // Attacking
            if (collision.gameObject.CompareTag("Player"))
            {
                Attack(collision.gameObject.GetComponent<IDamageable>());
            }
        }



        public float TakeDamage(float amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                Die();
            }

            _healthBar.value = _currentHealth / _enemy.MaxHealth;

            return _currentHealth;
        }

        private void Die()
        {
            _rigidbody2D.DOKill();
            foreach(var drop in _enemy.Drops)
            {
                Instantiate(drop.Prefab, new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)), Quaternion.identity);
            }
            Destroy(gameObject);
        }



        public void OnPointerClick(PointerEventData eventData)
        {
            EventManager.OnEnemyClicked?.Invoke(this);
        }
    }
}
