using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CaveGame
{
    public class OxygenTimer : MonoBehaviour
    {
        [SerializeField] private Image _stageOne, _stageTwo, _stageThree;
        [SerializeField] private float _stageZeroTime, _stageOneTime, _stageTwoTime, _stageThreeTime;

        private IEnumerator StageZero(float time)
        {
            yield return new WaitForSeconds(time);
            StartCoroutine(StageOne(_stageOneTime, _stageOne));
        }

        private IEnumerator StageOne(float time, Image image)
        {
            Debug.Log("Starting stage one of oxygen timer");
            image.gameObject.SetActive(true);
            float counter = 0;
            while (counter < time)
            {
                counter += Time.deltaTime;
                image.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, counter / time);

                yield return null;
            }
            StartCoroutine(StageTwo(_stageTwoTime, _stageTwo));
        }

        private IEnumerator StageTwo(float time, Image image)
        {
            Debug.Log("Starting stage two of oxygen timer");
            image.gameObject.SetActive(true);
            float counter = 0;
            while (counter < time)
            {
                counter += Time.deltaTime;
                image.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, counter / time);

                yield return null;
            }
            StartCoroutine(StageThree(_stageThreeTime, _stageThree));

        }

        private IEnumerator StageThree(float time, Image image)
        {
            Debug.Log("Starting stage three of oxygen timer");
            image.gameObject.SetActive(true);
            float counter = 0;
            while (counter < time)
            {
                counter += Time.deltaTime;
                image.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, counter / time);

                yield return null;
            }
        }

        private void Start()
        {
            StartCoroutine(StageZero(_stageZeroTime));
        }
    }
}
