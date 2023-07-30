using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class GuardedTerritory : MonoBehaviour
{
    private float _volumeAmplification = 0.5f;
    private int _minVolume = 0;
    private int _maxVolume = 1;
    private int _targetValue;
    private AudioSource _sound;
    private Coroutine _volumeRoutine;

    void Start()
    {
        _sound = GetComponent<AudioSource>();
        _sound.volume = 0;
    }

    private IEnumerator ChangeVolume()
    {
        var amplificationRate = new WaitForSeconds(0.1f);

        while (_sound.volume != _targetValue)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, _targetValue, _volumeAmplification * Time.deltaTime);
            yield return amplificationRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Robber>(out Robber component))
        {
            if (_volumeRoutine != null)
            {
                StopCoroutine(_volumeRoutine);
            }

            _sound.Play();
            _targetValue = _maxVolume;
            _volumeRoutine = StartCoroutine(ChangeVolume());
        }

        Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Robber>(out Robber component))
        {
            if (_volumeRoutine != null)
            {
                StopCoroutine(_volumeRoutine);
            }

            _targetValue = _minVolume;
            _volumeRoutine = StartCoroutine(ChangeVolume());
        }

        Debug.Log("left Collision");
    }


}
