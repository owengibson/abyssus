using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class Drill : MonoBehaviour
    {
        [SerializeField] private WaterPlayerController _player;
        [SerializeField] private AudioSource _rockBreaksfx;

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Ground")) return;

            foreach (var contactPoint in collision.contacts)
            {
                EventManager.OnTerrainEdit?.Invoke(contactPoint.point);
                Debug.Log("Drill");
                _rockBreaksfx.Play();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Ground")) return;

            foreach (var contactPoint in collision.contacts)
            {
                EventManager.OnTerrainEdit?.Invoke(contactPoint.point);
                Debug.Log("Drill2");
                _rockBreaksfx.Play();
            }

            _player.DrillBounceback(collision.contacts[0].normal);
        }
    }
}
