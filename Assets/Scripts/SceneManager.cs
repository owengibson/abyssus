using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CaveGame
{
    public class SceneManager : MonoBehaviour
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
            bool isTutorialActive = (bool)EventManager.OnCheckCaveTutorialState?.Invoke();
            if (isTutorialActive) return;

            _isInArea = true;
            EventManager.OnEnterOrExitStartingArea?.Invoke(true);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) && _isInArea)
            {
                GameStats.Instance.Stats.CavesVisited++;
                LoadScene("SubmarineScene");
            }
        }

        public void LoadScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
