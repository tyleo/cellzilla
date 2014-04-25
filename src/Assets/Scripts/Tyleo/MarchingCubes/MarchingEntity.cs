using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public abstract class MarchingEntity :
        MonoBehaviour
    {
        public abstract float GetIntensity(Vector3 entityEnvironmentSpaceVertex, Vector3 pointEnvironmentSpaceVertex);
    }
}
