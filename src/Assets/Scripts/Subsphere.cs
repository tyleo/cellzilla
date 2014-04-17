using System;
using UnityEngine;

[Serializable]
public sealed class Subsphere :
    MonoBehaviour
{
    [SerializeField]
    private int _groupTag = 0;

    private float _radius;

    public int GroupTag { get { return _groupTag; } }
    public float Radius { get { return _radius; } }

    public void Start()
    {
        Vector3 localScale = transform.localScale;
        if (localScale.x != localScale.y || localScale.y != localScale.z)
        {
            throw new Exception("Invalid scale.");
        }

        _radius = localScale.x / 2;
    }
}
