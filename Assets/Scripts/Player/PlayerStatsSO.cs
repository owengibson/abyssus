using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New Player Stats", menuName = "Player Stats")]
    public class PlayerStatsSO : ScriptableObject
    {
        public float MaxHealth;
        public float Damage;
        public float AttackRange;
        public float AttackCooldown;
    }
}
