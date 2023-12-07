using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CaveGame
{
    public class ReturnAnchor : MonoBehaviour
    {
        private bool _isInArea = false;
        private bool _canLeave = false;

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            _isInArea = false;
            _canLeave = true;
            EventManager.OnEnterOrExitStartingArea?.Invoke(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            if (!_canLeave) return;

            _isInArea = true;
            EventManager.OnEnterOrExitStartingArea?.Invoke(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isInArea)
            {
                GameStats.Instance.Stats.CavesVisited++;
                UnityEngine.SceneManagement.SceneManager.LoadScene("SubmarineScene");
            }
        }

        private void Start()
        {
            EventManager.OnAnchorSpawn?.Invoke(this);
        }
    }
}
