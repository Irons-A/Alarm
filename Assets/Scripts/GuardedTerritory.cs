using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardedTerritory : MonoBehaviour
{
    private float _volumeAmplification = 0.5f;
    private int _minVolume = 0;
    private int _maxVolume = 1;
    private int _targetValue;
    private AudioSource _sound;

    void Start()
    {
        _sound = GetComponent<AudioSource>();
        _sound.volume = 0;
    }

    void Update()
    {

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Robber>(out Robber component))
        {
            _sound.Play();
            _targetValue = _maxVolume;
            StartCoroutine(ChangeVolume());
        }

        Debug.Log(collision.gameObject.name);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Robber>(out Robber component))
        {
            _targetValue = _minVolume;
            StartCoroutine(ChangeVolume());
        }

        Debug.Log("left Collision");
    }


}
