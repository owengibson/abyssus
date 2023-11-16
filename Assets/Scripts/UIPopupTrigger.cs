using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class UIPopupTrigger : MonoBehaviour
    {
        [SerializeField] private Transform _uiElement;
        [SerializeField] private Vector3 _hiddenPosition;
        [SerializeField] private Vector3 _shownPosition;

        private float _transitionSpeed = 0.4f;
        private bool _isTriggered = false;

        private enum UIElementDisplayState { Hidden, InProgress, Shown };
        private UIElementDisplayState _displayState = UIElementDisplayState.Hidden;

        private void ToggleUIElement(ref UIElementDisplayState state)
        {
            switch (state)
            {
                case UIElementDisplayState.Hidden:
                    state = UIElementDisplayState.InProgress;
                    _uiElement.DOLocalMove(_shownPosition, _transitionSpeed).SetEase(Ease.OutBack).OnComplete(() => _displayState = UIElementDisplayState.Shown);
                    break;

                case UIElementDisplayState.InProgress:
                    break;

                case UIElementDisplayState.Shown:
                    state = UIElementDisplayState.InProgress;
                    _uiElement.DOLocalMove(_hiddenPosition, _transitionSpeed).SetEase(Ease.InBack).OnComplete(() => _displayState = UIElementDisplayState.Hidden);
                    break;

                default:
                    Debug.LogWarning("Invalid UIElementDisplayState");
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            _isTriggered = true;
            ToggleUIElement(ref _displayState);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            _isTriggered = false;
            ToggleUIElement(ref _displayState);
        }

        private void Update()
        {
            if (_isTriggered && _displayState == UIElementDisplayState.Hidden) ToggleUIElement(ref _displayState);
            else if (!_isTriggered && _displayState == UIElementDisplayState.Shown) ToggleUIElement(ref _displayState);
        }
    }
}
