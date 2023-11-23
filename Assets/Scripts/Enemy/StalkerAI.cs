using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class StalkerAI : MonoBehaviour
    {
        [SerializeField] private float _chaseSpeed;

        private Transform _player;
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private AudioSource _stalkerTrack;
        [SerializeField] private float _audioFadeIn;
        [SerializeField] private float _maxVolume;

        private void Awake()
        {
            _player = PlayerSpawner.Instance.Player.transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();

            InvokeRepeating("UpdateTarget", 0.5f, 0.5f);
            FadeInVolume();
        }

        private void UpdateTarget()
        {
            _rigidbody2D.DOKill();
            _rigidbody2D.DOMove(_player.position, _chaseSpeed).SetSpeedBased(true).SetEase(Ease.Linear);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            collision.GetComponent<IDamageable>().TakeDamage(1000);
        }
        public void FadeInVolume()
        {
            StartCoroutine(FadeAudio(_stalkerTrack, _audioFadeIn, _maxVolume));
        }


        IEnumerator FadeAudio(AudioSource audio, float duration, float maxVol)
        {
            float startVolume = 0f;
            audio.volume = 0f;
            audio.Play();

            float timer = 0.0f;

            while (timer < duration)
            {
                timer += Time.deltaTime;
                float volume = Mathf.Lerp(startVolume, maxVol, timer / duration);
                audio.volume = Mathf.Clamp01(volume);
                yield return null;
            }
        }
    }
    
}
