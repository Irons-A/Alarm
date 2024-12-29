using System;
using UnityEngine;

public class GuardedTerritory : MonoBehaviour
{
    public event Action alarmTriggering;
    public event Action alarmStopping;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Robber>(out Robber component))
        {
            alarmTriggering?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Robber>(out Robber component))
        {
            alarmStopping?.Invoke();
        }
    }
}
