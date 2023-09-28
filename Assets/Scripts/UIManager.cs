using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _terrainModeBorder;
        [SerializeField] private GameObject _returnToBoatPopup;

        private void ToggleTerrainModeBorder()
        {
            _terrainModeBorder.SetActive(!_terrainModeBorder.activeSelf);
        }

        private void ToggleReturnToBoatPopup(bool state)
        {
            _returnToBoatPopup.SetActive(state);
        }

        private void OnEnable()
        {
            EventManager.OnTerrainModeToggle += ToggleTerrainModeBorder;
            EventManager.OnEnterOrExitStartingArea += ToggleReturnToBoatPopup;
        }
        private void OnDisable()
        {
            EventManager.OnTerrainModeToggle -= ToggleTerrainModeBorder;
            EventManager.OnEnterOrExitStartingArea -= ToggleReturnToBoatPopup;
        }
    }
}
