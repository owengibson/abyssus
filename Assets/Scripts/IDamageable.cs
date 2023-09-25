using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CaveGame
{
    public interface IDamageable
    {
        /// <summary>
        /// Takes damage and returns resulting health.
        /// </summary>
        /// <param name="amount">Amount of damage to be taken.</param>
        /// <returns>The new current health of damaged object.</returns>
        public int TakeDamage(int amount);
    }
}
