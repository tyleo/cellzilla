using UnityEngine;
using System.Collections;

public class UDLRScript :
    MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.localPosition =
                new Vector3(
                    transform.localPosition.x,
                    transform.localPosition.y + _speed * Time.deltaTime,
                    transform.localPosition.z
                );
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.localPosition =
                new Vector3(
                    transform.localPosition.x,
                    transform.localPosition.y - _speed * Time.deltaTime,
                    transform.localPosition.z
                );
        }
    }
}
