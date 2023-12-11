using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CaveGame
{
    public class CaveTutorialManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tutorialText;

        private int _tutorialIndex = 1;
        private GameObject _anchor;

        private static bool _isTutorialCompleted = false;

        private void Start()
        {
            if (_isTutorialCompleted)
            {
                Destroy(_tutorialText.transform.parent.gameObject);
            }
        }

        private void GetAndDisableAnchorAndPlayer(ReturnAnchor anchor)
        {
            if (_isTutorialCompleted) return;

            _anchor = anchor.gameObject;
            _anchor.SetActive(false);

            PlayerSpawner.Instance.Player.GetComponent<WaterPlayerController>().ToggleMovement(false);
        }

        private void UpdateTutorialPanel(string text, bool isLastStep = false)
        {
            _tutorialIndex++;

            if (isLastStep)
            {
                _isTutorialCompleted = true;
                PlayerSpawner.Instance.Player.GetComponent<WaterPlayerController>().ToggleMovement(true);
                _tutorialText.transform.parent.DOMoveY(-300, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    Destroy(_tutorialText.transform.parent.gameObject);
                    _anchor.SetActive(true);
                });
            }
            else
            {
                _tutorialText.DOText(text, 1f);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_tutorialIndex == 1) UpdateTutorialPanel("You will have to dive deep into the cave to find the treasures within.");
                else if (_tutorialIndex == 2) UpdateTutorialPanel("Left click to shoot, E to drill through terrain, TAB to open your inventory.");
                else if (_tutorialIndex == 3) UpdateTutorialPanel("Remember to return to the anchor before your time runs out...");
                else if (_tutorialIndex == 4) UpdateTutorialPanel("", true);
            }
        }

        private bool IsTutorialActive()
        {
            if(_tutorialIndex < 5) return true;
            else return false;
        }

        private void OnEnable()
        {
            EventManager.OnCheckCaveTutorialState += IsTutorialActive;
            EventManager.OnAnchorSpawn += GetAndDisableAnchorAndPlayer;
        }
        private void OnDisable()
        {
            EventManager.OnCheckCaveTutorialState -= IsTutorialActive;
            EventManager.OnAnchorSpawn -= GetAndDisableAnchorAndPlayer;
        }
    }
}
