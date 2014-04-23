using UnityEngine;

public sealed class Test :
    MonoBehaviour
{
    [SerializeField]
    private GameObject _objectThatCanEnterMe;

    private void Update()
    {
        Debug.Log(ObjectIsInMe(transform, _objectThatCanEnterMe.transform));
    }

    private static bool ObjectIsInMe(Transform myTransform, Transform otherTransform)
    {
        var meToOtherVector = otherTransform.position - myTransform.position;
        var rotatedMeToOtherVector = myTransform.rotation * meToOtherVector;

        return
            rotatedMeToOtherVector.x > -0.5f * myTransform.lossyScale.x &&
            rotatedMeToOtherVector.x < +0.5f * myTransform.lossyScale.x &&
            rotatedMeToOtherVector.y > -0.5f * myTransform.lossyScale.y &&
            rotatedMeToOtherVector.y < +0.5f * myTransform.lossyScale.y &&
            rotatedMeToOtherVector.z > -0.5f * myTransform.lossyScale.z &&
            rotatedMeToOtherVector.z < +0.5f * myTransform.lossyScale.z;
    }
}
