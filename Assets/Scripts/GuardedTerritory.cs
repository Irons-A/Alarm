using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuardedTerritory : MonoBehaviour
{
    public static UnityAction triggerAlarm;
    public static UnityAction stopAlarm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Robber>(out Robber component))
        {
            triggerAlarm?.Invoke();
        }

        Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Robber>(out Robber component))
        {
            stopAlarm?.Invoke();
        }

        Debug.Log("left Collision");
    }
}
