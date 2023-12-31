using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
    public class EnemySO : ScriptableObject
    {
        public GameObject Prefab;
        public string Name;
        public float MaxHealth;
        public float Damage;
        public float ChaseSpeed;
        public float AttackCooldown;
        public float AttackRange;
        public float DetectionRadius;
        public int PatrolDistance;
        public EnemyDropItemSO[] Drops;
    }
}
