using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public abstract class MarchingEntity :
        MonoBehaviour
    {
        private Vector3 _environmentSpaceCoordinates = Vector3.zero;
        public Vector3 EnvironmentSpaceCoordinates { get { return _environmentSpaceCoordinates; } }

        public void SetEnvironmentSpaceCoordinates(Transform environmentTransform)
        {
            _environmentSpaceCoordinates = environmentTransform.InverseTransformPoint(transform.position);
        }

        public abstract float GetIntensity(Vector3 pointWorldSpaceVertex);
    }
}
