using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AlarmManager : MonoBehaviour
{
    private float _volumeAmplification = 0.5f;
    private int _minVolume = 0;
    private int _maxVolume = 1;
    private int _targetValue;
    private AudioSource _sound;
    private Coroutine _volumeRoutine;
    private bool isRobberInside;

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
        
        if (_volumeRoutine != null)
        {
            StopCoroutine(_volumeRoutine);
        }

        while (_sound.volume != _targetValue)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, _targetValue, _volumeAmplification * Time.deltaTime);
            yield return amplificationRate;
        }
    }

    public void TriggerAlarm()
    {
        _targetValue = _maxVolume;
        _volumeRoutine = StartCoroutine(ChangeVolume());
    }

    public void StopAlarm()
    {
        _targetValue = _minVolume;
        _volumeRoutine = StartCoroutine(ChangeVolume());
    }
}
