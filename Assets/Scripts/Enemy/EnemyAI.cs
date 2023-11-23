using DG.Tweening;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CaveGame
{
    public class EnemyAI : Enemy, IDamageable, IPointerClickHandler
    {
        public EnemyState CurrentState = EnemyState.Patrol;
        public Transform Target;
        [SerializeField] private AudioSource _hitSfx;
       

        public EnemySO Enemy;
        [Space(20)] 

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Slider _healthBar;
        [Space]

        //[SerializeField] private Transform _target;
        [SerializeField] private float _nextWaypointDistance = 3f;
        [SerializeField] private string _graphMask;

        private float _currentHealth;

        private Path _path;
        private int _currentWaypoint = 0;

        private bool _isAttackOnCooldown = false;

        private Seeker _seeker;

        private void Start()
        {
            _currentHealth = Enemy.MaxHealth;

            Target = PlayerSpawner.Instance.Player.transform;

            _seeker = GetComponent<Seeker>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _seeker.graphMask = GraphMask.FromGraphName(_graphMask);
            //StartCoroutine(UpdatePath());
        }

        private IEnumerator UpdatePath()
        {
            while(true)
            {
                if (_seeker.IsDone())
                {
                    _seeker.StartPath(_rigidbody2D.position, Target.position, OnPathComplete);
                }
                yield return new WaitForSeconds(0.5f);
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
            if (CurrentState == EnemyState.Chase)
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
        }

        private void Update()
        {
            /*if (Target == null)
            {
                Target = PlayerSpawner.Instance.Player.transform;
            }*/

            if (Vector2.Distance(_rigidbody2D.position, Target.position) <= Enemy.AttackRange && CurrentState != EnemyState.Attack && !_isAttackOnCooldown)
            {
                // ATTACK
                StartCoroutine(Attack(Target.GetComponent<IDamageable>()));
            }

            if (Vector2.Distance(_rigidbody2D.position, Target.position) <= Enemy.DetectionRadius && CurrentState != EnemyState.Chase)
            {
                // START CHASING
                Chase();
            }
            else if (Vector2.Distance(_rigidbody2D.position, Target.position) > Enemy.DetectionRadius && CurrentState == EnemyState.Chase)
            {
                // STOP CHASING (start "patrolling")
                StopCoroutine(UpdatePath());
                CurrentState = EnemyState.Patrol;
            }
        }

        private void Chase()
        {
            CurrentState = EnemyState.Chase;
            //StartCoroutine(ChaseExitBuffer());
            StartCoroutine(UpdatePath());
        }

        private IEnumerator Attack(IDamageable target)
        {
            _isAttackOnCooldown = true;

            CurrentState = EnemyState.Attack;
            target.TakeDamage(Enemy.Damage);
            Debug.Log("Attacked " + target);

            yield return new WaitForSeconds(Enemy.AttackCooldown);
            CurrentState = EnemyState.Chase;
            _isAttackOnCooldown = false;
        }

        public float TakeDamage(float amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                Die();
            }

            if (_currentHealth != Enemy.MaxHealth)
            {
                _hitSfx.Play();
                _healthBar.gameObject.SetActive(true);
            }

            _healthBar.value = _currentHealth / Enemy.MaxHealth;

            return _currentHealth;
        }

        private void Die()
        {
            
            foreach (var drop in Enemy.Drops)
            {
                
                Instantiate(drop.Prefab, new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f)), Quaternion.identity);
            }
            Destroy(gameObject);
           
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            EventManager.OnEnemyClicked?.Invoke(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_rigidbody2D.position, Enemy.DetectionRadius);
        }
    }
}
