using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        private void OnEnable()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
            CurrentHealth = MaxHealth;
        }

        
    }
}
