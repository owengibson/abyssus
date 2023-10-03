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

        private void ToggleTerrainModeBorder()
        {
            _terrainModeBorder.SetActive(!_terrainModeBorder.activeSelf);
        }

        private void ToggleReturnToBoatPopup(bool state)
        {
            _returnToBoatPopup.SetActive(state);
        }

        private void UpdatePlayerHealthBar(float currentHealth, float maxHealth)
        {
            _playerHealthBar.value = currentHealth / maxHealth;
        }

        private void OnEnable()
        {
            EventManager.OnTerrainModeToggle += ToggleTerrainModeBorder;
            EventManager.OnEnterOrExitStartingArea += ToggleReturnToBoatPopup;
            EventManager.OnPlayerTakeDamage += UpdatePlayerHealthBar;
        }
        private void OnDisable()
        {
            EventManager.OnTerrainModeToggle -= ToggleTerrainModeBorder;
            EventManager.OnEnterOrExitStartingArea -= ToggleReturnToBoatPopup;
            EventManager.OnPlayerTakeDamage -= UpdatePlayerHealthBar;
        }
    }
}
