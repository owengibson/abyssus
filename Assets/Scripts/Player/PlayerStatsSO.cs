using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New Player Stats", menuName = "Player Stats")]
    public class PlayerStatsSO : ScriptableObject
    {
        public float MaxHealth;
        public float CurrentHealth;
        public float Damage;
        public float AttackRange;
        public float AttackCooldown;

        private void Awake()
        {
            if (CurrentHealth <= 0)
            {
                CurrentHealth = MaxHealth;
            }
        }

        private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}
