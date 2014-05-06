using UnityEngine;

namespace Tyleo.MarchingCubes
{
    /// <summary>
    /// An entity which participates in determining the geometry of a mesh in the marching cubes
    /// algorithm.
    /// </summary>
    public abstract class MarchingEntity :
        MonoBehaviour
    {
        private Vector3 _environmentSpaceCoordinates = Vector3.zero;
        /// <summary>
        /// The local-space coordinates of this MarchingEntitiy within a MarchingCubesEnvironment.
        /// </summary>
        public Vector3 EnvironmentSpaceCoordinates { get { return _environmentSpaceCoordinates; } }

        /// <summary>
        /// Calculates and sets the EnvironmentSpaceCoordinates of this MarchingEntity.
        /// </summary>
        /// <param name="environmentTransform"></param>
        public void SetEnvironmentSpaceCoordinates(Transform environmentTransform)
        {
            _environmentSpaceCoordinates = environmentTransform.InverseTransformPoint(transform.position);
        }

        /// <summary>
        /// Provides the intensity at a vertex in world-space.
        /// </summary>
        /// <param name="pointWorldSpaceVertex">
        /// The world-space position of the vertex.
        /// </param>
        /// <returns>
        /// The intensity provided by this MarchingEntity at the vertex in world-space.
        /// </returns>
        public abstract float GetIntensity(Vector3 pointWorldSpaceVertex);
    }
}
