using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CaveGame
{
    public class PauseMenuManager : MonoBehaviour
    {
        #region SINGLETON
        public static PauseMenuManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        [SerializeField] private GameObject _canvas;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePauseMenu(!_canvas.activeSelf);
            }
        }

        public void TogglePauseMenu(bool activate)
        {
            _canvas.SetActive(activate);
            Time.timeScale = activate ? 0f : 1f;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void CheckForIllegalScene(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "EndScene" || scene.name == "TitleScene") Destroy(gameObject);
        }

        private void OnEnable() => UnityEngine.SceneManagement.SceneManager.sceneLoaded += CheckForIllegalScene;
        private void OnDisable() => UnityEngine.SceneManagement.SceneManager.sceneLoaded -= CheckForIllegalScene;
    }
}
