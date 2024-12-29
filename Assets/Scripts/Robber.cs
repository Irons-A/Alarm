using UnityEngine;

public class Robber : MonoBehaviour
{
    private const KeyCode RightInput = KeyCode.D;
    private const KeyCode LeftInput = KeyCode.A;

    private float _speed = 6;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(RightInput))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(LeftInput))
        {
            transform.Translate(-1 * _speed * Time.deltaTime, 0, 0);
        }
    }
}
