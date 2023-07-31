using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuardedTerritory : MonoBehaviour
{
    [SerializeField] private UnityEvent _triggerAlarm;
    [SerializeField] private UnityEvent _stopAlarm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Robber>(out Robber component))
        {
            _triggerAlarm?.Invoke();
        }

        Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Robber>(out Robber component))
        {
            _stopAlarm?.Invoke();
        }

        Debug.Log("left Collision");
    }
}
