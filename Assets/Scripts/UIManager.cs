using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CaveGame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _terrainModeBorder;
        [SerializeField] private GameObject _returnToBoatPopup;
        [SerializeField] private Slider _playerHealthBar;
        [SerializeField] private GameObject _deathScreen;

        /*private void ToggleTerrainModeBorder()
        {
            _terrainModeBorder.SetActive(!_terrainModeBorder.activeSelf);
        }*/

        private void ToggleReturnToBoatPopup(bool state)
        {
            _returnToBoatPopup.SetActive(state);
        }

        private void UpdatePlayerHealthBar(float currentHealth, float maxHealth)
        {
            _playerHealthBar.value = currentHealth / maxHealth;
        }

        private void ShowDeathScreen()
        {
            CanvasGroup canvasGroup = _deathScreen.GetComponent<CanvasGroup>();
            _deathScreen.SetActive(true);
            canvasGroup.DOFade(1, 1);
        }

        private void OnEnable()
        {
            EventManager.OnEnterOrExitStartingArea += ToggleReturnToBoatPopup;
            EventManager.OnPlayerTakeDamage += UpdatePlayerHealthBar;
            EventManager.OnPlayerDeath += ShowDeathScreen;
        }
        private void OnDisable()
        {
            EventManager.OnEnterOrExitStartingArea -= ToggleReturnToBoatPopup;
            EventManager.OnPlayerTakeDamage -= UpdatePlayerHealthBar;
            EventManager.OnPlayerDeath -= ShowDeathScreen;
        }
    }
}
