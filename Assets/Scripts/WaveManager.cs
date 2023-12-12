using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CaveGame
{
    public class WaveManager : SerializedMonoBehaviour
    {
        [SerializeField] private WaveScalingStatsSO _waveScalingStats;
        [Space(30)]

        [SerializeField] private Transform[] _spawnPoints;
        [Space]

        [SerializeField] private float _timeBetweenWaves;
        [SerializeField] private float _timeBetweenEnemies;
        [SerializeField] private Transform _enemyTarget;
        [SerializeField] private AudioSource _waveSoundtrack;
        [SerializeField] private AudioSource _safeSoundtrack;
        [SerializeField] private float _soundtrackFade;
        [Space]

        [SerializeField] private GameObject _leaveButton;
        [SerializeField] private CameraMove _cameraMove;

        private void Start()
        {
            if (GameStats.Instance.Stats.CavesVisited % 2 == 0 && GameStats.Instance.Stats.CavesVisited != 0)
            {
                // Scaling
                if (_waveScalingStats.WaveRoundCount != 0 && _waveScalingStats.WaveRoundCount % 2 == 0)
                {
                    _waveScalingStats.ScaleWaves();
                }

                StartCoroutine(SpawnWaves());
            }
            else
            {
                _safeSoundtrack.Play();
            }
            if (GameStats.Instance.Stats.CavesVisited == 0 && !SubmarineTutorialManager.IsTutorialCompleted)
            {
                _leaveButton.SetActive(false);
            }
        }

        private IEnumerator SpawnWaves()
        {
            Debug.Log("Spawning waves...");
            _cameraMove.MoveCameraForWaveCombat();
            _leaveButton.SetActive(false);

            _waveScalingStats.WaveRoundCount++;
            _waveSoundtrack.Play(); 
            foreach (var wave in _waveScalingStats.Waves)
            {
                foreach (var enemyType in wave.Keys)
                {
                    for (int i = 0; i < wave[enemyType]; i++)
                    {
                        var spawnedEnemy = Instantiate(enemyType.Prefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
                        var spawnedEnemyAI = spawnedEnemy.GetComponent<WaveEnemyAI>();
                        spawnedEnemyAI.Target = _enemyTarget;
                        spawnedEnemyAI.Enemy = enemyType;

                        yield return new WaitForSeconds(_timeBetweenEnemies);
                    }
                }

                yield return new WaitForSeconds(_timeBetweenWaves);
            }
            yield return new WaitForSeconds(5);

            _leaveButton.SetActive(true);
            if (GameStats.Instance.Stats.CavesVisited == 0) EventManager.OnTutorialPromptCompleted?.Invoke(5);

            FadeOutVolume();    
        }
        public void FadeOutVolume()
        {
            StartCoroutine(FadeAudio(_waveSoundtrack, _soundtrackFade));
        }

        private void StartWaveCombat()
        {
            StartCoroutine(SpawnWaves());
        }

      
        IEnumerator FadeAudio(AudioSource audio, float duration)
        {
            float startVolume = audio.volume;
            float timer = 0.0f;

            while (timer < duration)
            {
                timer += Time.deltaTime;
                audio.volume = Mathf.Lerp(startVolume, 0f, timer / duration);
                yield return null;
            }
            audio.Stop(); 
        }

        private void OnEnable()
        {
            EventManager.OnTutorialTurretEntered += StartWaveCombat;
        }
        private void OnDisable()
        {
            EventManager.OnTutorialTurretEntered -= StartWaveCombat;
        }
    }
}
