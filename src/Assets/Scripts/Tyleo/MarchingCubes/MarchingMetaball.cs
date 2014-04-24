using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingMetaball :
        MarchingEntity
    {
        /// <summary>
        /// Provides an intensity which decreases from the center of the metaball.
        /// </summary>
        /// <param name="worldSpaceVertex">
        /// A vertex represent the world-space coordinates of the point to calculate the intensity
        /// at.
        /// </param>
        /// <returns>
        /// An intensity which decreases from the center of the metaball.
        /// </returns>
        public sealed override float GetIntensity(Vector3 worldSpaceVertex)
        {
            var metaballToWorldSpaceVertex = transform.InverseTransformPoint(worldSpaceVertex);

            return 0.5f / metaballToWorldSpaceVertex.sqrMagnitude;
        }
    }
}
