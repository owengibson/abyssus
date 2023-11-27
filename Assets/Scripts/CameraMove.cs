using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private float _nonWaveCameraSize;
        [SerializeField] private float _waveCameraSize;

        private void Start()
        {
            if (GameStats.Instance.Stats.CavesVisited % 2 == 0 && GameStats.Instance.Stats.CavesVisited != 0)
            {
                GetComponent<Camera>().DOOrthoSize(_waveCameraSize, 2f);
                transform.DOMoveY(3.2f, 2f);
            }
        }
    }
}
