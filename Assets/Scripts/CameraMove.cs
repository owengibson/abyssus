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

        public void MoveCameraForWaveCombat()
        {
            Debug.Log("Moving camera...");
            //GetComponent<Camera>().DOOrthoSize(_waveCameraSize, 2f);
            //transform.DOMoveY(3.2f, 2f);
            Camera camera = GetComponent<Camera>();
            StartCoroutine(MoveOrthoSize(camera, camera.orthographicSize, _waveCameraSize, 2f));
            StartCoroutine(MoveCameraY(camera.transform, camera.transform.position.y, 3.2f, 2f));
        }

        private IEnumerator MoveOrthoSize(Camera camera, float start, float end, float time)
        {
            float counter = 0f;

            while (counter < time)
            {
                counter += Time.deltaTime;
                camera.orthographicSize = Mathf.Lerp(start, end, counter / time);

                yield return null;
            }
        }

        private IEnumerator MoveCameraY(Transform camera, float start, float end, float time)
        {
            float counter = 0f;

            while (counter < time)
            {
                counter += Time.deltaTime;
                camera.position = Vector2.Lerp(new Vector2(camera.position.x, start), new Vector2(camera.position.x, end), counter / time);

                yield return null;
            }
        }
    }
}
