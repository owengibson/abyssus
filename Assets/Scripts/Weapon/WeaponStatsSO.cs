using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    [CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Weapon Stats")]
    public class WeaponStatsSO : ScriptableObject
    {
        public float Damage;
        public float AttackCooldown;
    }
}
