using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(GuardedTerritory))]

public class Alarm : MonoBehaviour
{
    private float _volumeAmplification = 0.5f;
    private int _minVolume = 0;
    private int _maxVolume = 1;
    private int _targetValue;
    private AudioSource _sound;
    private Coroutine _volumeRoutine;
    [SerializeField] private GuardedTerritory _guardedTerritory;

    private void OnEnable()
    {
        _guardedTerritory.AlarmTriggered += OnAlarmTrigger;
        _guardedTerritory.AlarmStopped += OnAlarmStop;
    }
    private void OnDisable()
    {
        _guardedTerritory.AlarmTriggered -= OnAlarmTrigger;
        _guardedTerritory.AlarmStopped -= OnAlarmStop;
    }

    private void Start()
    {
        _sound = GetComponent<AudioSource>();
        _sound.volume = 0;
    }

    private IEnumerator ChangeVolume()
    {
        var amplificationRate = new WaitForSeconds(0.1f);

        if (!_sound.isPlaying)
        {
            _sound.Play();
        }

        while (_sound.volume != _targetValue)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, _targetValue, _volumeAmplification * Time.deltaTime);
            yield return amplificationRate;
        }
    }

    private void StopOldCoroutine()
    {
        if (_volumeRoutine != null)
        {
            StopCoroutine(_volumeRoutine);
        }
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
}
