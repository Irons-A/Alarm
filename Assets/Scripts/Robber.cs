using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber : MonoBehaviour
{
    private float _speed = 6;
    
    void Start()
    {
        
    }

    void Update()
    {
        MovementMechanic();
    }

    private void MovementMechanic()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-1 * _speed * Time.deltaTime, 0, 0);
        }
    }
}
