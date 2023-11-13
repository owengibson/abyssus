using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class Drill : MonoBehaviour
    {
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Ground")) return;

            foreach (var contactPoint in collision.contacts)
            {
                EventManager.OnTerrainEdit?.Invoke(contactPoint.point);
            }
        }
    }
}
