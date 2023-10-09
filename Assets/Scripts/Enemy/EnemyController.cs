using DG.Tweening;
using Pathfinding;
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
        public Transform PrimaryTarget;

        [SerializeField] private EnemySO _enemy;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Slider _healthBar;
        [Space]

        [SerializeField] private float _detectionRadius = 15f;
        [SerializeField] private float _patrolCycleLength = 4f;
        [SerializeField] private float _chaseSpeed = 200f;
        [SerializeField] private float _nextWaypointDistance = 3f;

        private float _currentHealth;

        private PatrolDirection _patrolDirection = PatrolDirection.Right;
        private Vector3 _spawnPosition;
        private Rigidbody2D _rigidbody2D;

        private Path _path;
        private Seeker _seeker;
        private int _currentWaypoint = 0;
        private bool _hasReachedEndOfPath = false;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _seeker = GetComponent<Seeker>();

            _currentHealth = _enemy.MaxHealth;
            _spawnPosition = transform.position;

            Invoke("Patrol", Random.Range(0, 2));

        }

        private void Update()
        {

            /*if (CurrentState != EnemyState.Chase && Vector2.Distance(transform.position, PlayerSpawner.Instance.Player.transform.position) <= _detectionRadius)
            {
                Chase();
            }
            else if (CurrentState == EnemyState.Chase && Vector2.Distance(transform.position, PlayerSpawner.Instance.Player.transform.position) > _detectionRadius)
            {
                Patrol();
            }*/
        }

        private void FixedUpdate()
        {
            if (CurrentState == EnemyState.Chase && _path != null)
            {
                if (_currentWaypoint >= _path.vectorPath.Count)
                {
                    _hasReachedEndOfPath = true;
                }
                else
                {
                    _hasReachedEndOfPath = false;
                }

                Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rigidbody2D.position).normalized;
                Vector2 force = direction * _chaseSpeed;
                _rigidbody2D.AddForce(force);

                float distance = Vector2.Distance(_rigidbody2D.position, _path.vectorPath[_currentWaypoint]);
                if (distance < _nextWaypointDistance)
                {
                    _currentWaypoint++;
                }
            }

            if (_rigidbody2D.velocity.x >= 0)
            {
                _spriteRenderer.flipX = false;
            }
            else
            {
                _spriteRenderer.flipX = true;
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

            //_seeker.StartPath(_rigidbody2D.position, _spawnPosition + new Vector3(_enemy.PatrolDistance, 0), PatrolReverse);
        }

        /*private void PatrolReverse(Path p)
        {
            _seeker.StartPath(_rigidbody2D.position, _spawnPosition, Patrol);
        }*/

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

        /*private void Chase()
        {
            _rigidbody2D.DOKill();
            CurrentState = EnemyState.Chase;

            StartCoroutine(UpdatePathOnTimer());
        }

        private IEnumerator UpdatePathOnTimer()
        {
            while(CurrentState == EnemyState.Chase)
            {
                UpdatePath(PrimaryTarget);
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void UpdatePath(Transform target)
        {
            if (_seeker.IsDone())
            {
                _seeker.StartPath(_rigidbody2D.position, target.position, OnPathComplete);
            }
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                _path = p;
                _currentWaypoint = 0;
            }
        }*/

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
