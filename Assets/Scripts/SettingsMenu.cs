using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class SettingsMenu : MonoBehaviour
    {
        public void ToggleSettingsPanel(bool isActive)
        {
            if (isActive)
            {
                transform.DOMoveY(-9, 0.5f).SetEase(Ease.InBack);
            }
            else
            {
                transform.DOMoveY(0, 0.5f).SetEase(Ease.OutBack);
            }
        }
    }
}
