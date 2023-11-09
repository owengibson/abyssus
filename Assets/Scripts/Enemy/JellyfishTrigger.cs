using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class JellyfishTrigger : MonoBehaviour
    {
        [SerializeField] private Jellyfish _jellyfish;
        public void TriggerExplosion()
        {
            _jellyfish.Explode();
        }
    }
}
