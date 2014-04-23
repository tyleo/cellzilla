using UnityEngine;

public sealed class Test :
    MonoBehaviour
{
    [SerializeField]
    private int _cubesAlongX = 20;
    [SerializeField]
    private int _cubesAlongY = 20;
    [SerializeField]
    private int _cubesAlongZ = 20;
    [SerializeField]
    private GameObject _objectThatCanEnterMe;

    private void Update()
    {
        Debug.Log(ObjectPositionToIndexVector(transform, _objectThatCanEnterMe.transform, _cubesAlongX, _cubesAlongY, _cubesAlongZ));
    }

    private static Vector3 ObjectPositionToIndexVector(Transform myTransform, Transform otherTransform, int cubesAlongX, int cubesAlongY, int cubesAlongZ)
    {
        var shiftedUnitIndexVector = myTransform.InverseTransformPoint(otherTransform.position);

        var unitIndexVector =
            shiftedUnitIndexVector +
            new Vector3(
                +0.5f,
                +0.5f,
                +0.5f
            );

        var indexVector =
            new Vector3(
                unitIndexVector.x * cubesAlongX,
                unitIndexVector.y * cubesAlongY,
                unitIndexVector.z * cubesAlongZ
            );

        return indexVector;
    }
}
