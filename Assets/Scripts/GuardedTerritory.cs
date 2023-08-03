using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuardedTerritory : MonoBehaviour
{
    public UnityAction AlarmTriggered;
    public UnityAction AlarmStopped;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Robber>(out Robber component))
        {
            AlarmTriggered?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Robber>(out Robber component))
        {
            AlarmStopped?.Invoke();
        }
    }
}
