using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CaveGame
{
    public class SubmarineTutorialManager : MonoBehaviour
    {
        public static bool IsTutorialCompleted = false;

        [SerializeField] private TextMeshProUGUI[] _textPrompts;
        [SerializeField] private GameObject _tutorialFinishedPanel;
        [SerializeField] private PlayerStatsSO subStats;

        private List<int> _completedPrompts = new List<int>();

        private void Start()
        {
            if (subStats != null)
            {
                subStats.Init(); 
                Debug.Log("Current Health was set to Max Health");
            }

            if (IsTutorialCompleted)
            {
                Destroy(gameObject);
            }
            else
            {
                // TODO 1/12/2023 3:45 - Trying to work out why the health does not reset
               
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
                IsTutorialCompleted = true;
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
