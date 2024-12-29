using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(GuardedTerritory))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private GuardedTerritory _guardedTerritory;

    private float _volumeAmplification = 0.5f;
    private int _minVolume = 0;
    private int _maxVolume = 1;
    private int _targetValue;
    private AudioSource _sound;
    private Coroutine _volumeRoutine;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
        _sound.volume = 0;
    }

    private void OnEnable()
    {
        _guardedTerritory.alarmTriggering += OnAlarmTrigger;
        _guardedTerritory.alarmStopping += OnAlarmStop;
    }

    private void OnDisable()
    {
        _guardedTerritory.alarmTriggering -= OnAlarmTrigger;
        _guardedTerritory.alarmStopping -= OnAlarmStop;
    }

    public void OnAlarmTrigger()
    {
        _targetValue = _maxVolume;
        StopOldCoroutine();
        _volumeRoutine = StartCoroutine(ChangeVolume());
    }

    public void OnAlarmStop()
    {
        _targetValue = _minVolume;
        StopOldCoroutine();
        _volumeRoutine = StartCoroutine(ChangeVolume());
    }

    private IEnumerator ChangeVolume()
    {
        if (_sound.isPlaying != true)
        {
            _sound.Play();
        }

        while (_sound.volume != _targetValue)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, _targetValue, _volumeAmplification * Time.deltaTime);
            yield return null;
        }
    }

    private void StopOldCoroutine()
    {
        if (_volumeRoutine != null)
        {
            StopCoroutine(_volumeRoutine);
        }
    }
}
