using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CaveGame
{
    public class SceneManager : MonoBehaviour
    {
        private bool _isAllowedToLeave = false;
        private bool _isInArea = false;

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            _isAllowedToLeave = true;
            _isInArea = false;
            EventManager.OnEnterOrExitStartingArea?.Invoke(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player") && !_isAllowedToLeave) return;

            _isInArea = true;

            if (_isAllowedToLeave)
            {
                EventManager.OnEnterOrExitStartingArea?.Invoke(true);
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Return) && _isInArea)
            {
                GameStats.Instance.Stats.CavesVisited++;

                if (GameStats.Instance.Stats.ItemsBought == 0)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("SubmarineScene");
                }
                else
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
                }
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
