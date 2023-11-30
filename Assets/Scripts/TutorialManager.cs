using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CaveGame
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _textPrompts;
        [SerializeField] private GameObject _tutorialFinishedPanel;

        private List<int> _completedPrompts = new List<int>();
        private static bool _isTutorialCompleted = false;

        private void Start()
        {
            if (_isTutorialCompleted)
            {
                Destroy(gameObject);
            }
            else
            {
                EventManager.OnSubmarineTakeDamage?.Invoke(10);
            }
        }

        private void CompleteTutorialPrompt(int index)
        {
            if (_completedPrompts.Contains(index)) return;

            if (index > _textPrompts.Length || index < 0)
            {
                Debug.Log("Invalid tutorial prompt index");
                return;
            }

            else if (index == 4)
            {
                // start waves
                if(_completedPrompts.Contains(0) && _completedPrompts.Contains(1) && _completedPrompts.Contains(2) && _completedPrompts.Contains(3))
                {
                    EventManager.OnTutorialTurretEntered?.Invoke();
                    _completedPrompts.Add(index);
                    _textPrompts[index].color = Color.red;
                }
                return;
            }
            else if (index == _textPrompts.Length - 1)
            {
                // finish tutorial
                _tutorialFinishedPanel.SetActive(true);
                _isTutorialCompleted = true;
            }
            _completedPrompts.Add(index);
            _textPrompts[index].color = Color.red;
        }

        private void OnEnable()
        {
            EventManager.OnTutorialPromptCompleted += CompleteTutorialPrompt;
        }
        private void OnDisable()
        {
            EventManager.OnTutorialPromptCompleted -= CompleteTutorialPrompt;
        }
    }
}
