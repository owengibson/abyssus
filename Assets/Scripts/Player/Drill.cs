using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class Drill : MonoBehaviour
    {
        [SerializeField] private WaterPlayerController _player;

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Ground")) return;

            foreach (var contactPoint in collision.contacts)
            {
                EventManager.OnTerrainEdit?.Invoke(contactPoint.point);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Ground")) return;

            foreach (var contactPoint in collision.contacts)
            {
                EventManager.OnTerrainEdit?.Invoke(contactPoint.point);
            }

            _player.DrillBounceback(collision.contacts[0].normal);
        }
    }
}
