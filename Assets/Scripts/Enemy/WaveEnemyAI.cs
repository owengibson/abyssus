using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CaveGame
{
    public class WaveEnemyAI : Enemy, IDamageable, IPointerClickHandler
    {
        public EnemySO Enemy;
        public Transform Target;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Slider _healthBar;
        [SerializeField] private float _nextWaypointDistance = 3f;

        private Path _path;
        private Seeker _seeker;

        private int _currentWaypoint = 0;
        private float _currentHealth;
        private bool _isAttackOnCooldown = false;

        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _currentHealth = Enemy.MaxHealth;

            _rigidbody2D = GetComponent<Rigidbody2D>();
            _seeker = GetComponent<Seeker>();

            InvokeRepeating("UpdatePath", 0, 0.5f);
        }

        private void UpdatePath()
        {
            if (_seeker.IsDone())
            {
                _seeker.StartPath(_rigidbody2D.position, Target.position, OnPathComplete);
            }
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                _path = p;
                _currentWaypoint = 0;
            }
        }

        private void FixedUpdate()
        {
            if (_path == null) return;

            if (_currentWaypoint >= _path.vectorPath.Count)
            {
                return;
            }

            Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rigidbody2D.position).normalized;
            Vector2 force = direction * Enemy.ChaseSpeed;

            _rigidbody2D.AddForce(force);

            float distance = Vector2.Distance(_rigidbody2D.position, _path.vectorPath[_currentWaypoint]);
            if (distance < _nextWaypointDistance)
            {
                _currentWaypoint++;
            }

            if (_rigidbody2D.velocity.x >= 0.01f)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_rigidbody2D.velocity.x <= -0.01f)
            {
                _spriteRenderer.flipX = true;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            EventManager.OnWaveEnemyClicked?.Invoke(this);
            Debug.Log("Clicked on " + gameObject.name);
        }

        public float TakeDamage(float amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                Die();
            }

            _healthBar.value = _currentHealth / Enemy.MaxHealth;

            return _currentHealth;
        }
        private void Die()
        {
            foreach (var drop in Enemy.Drops)
            {
                Instantiate(drop.Prefab, new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f)), Quaternion.identity);
            }
            EventManager.OnEnemyDie?.Invoke(gameObject);
            Destroy(gameObject);
        }

        private IEnumerator Attack(IDamageable target)
        {
            _isAttackOnCooldown = true;

            target.TakeDamage(Enemy.Damage);
            Debug.Log("Attacked " + target);

            yield return new WaitForSeconds(Enemy.AttackCooldown);
            _isAttackOnCooldown = false;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Submarine")) return;
            if (_isAttackOnCooldown) return;

            StartCoroutine(Attack(collision.gameObject.GetComponent<IDamageable>()));
        }
    }
}
