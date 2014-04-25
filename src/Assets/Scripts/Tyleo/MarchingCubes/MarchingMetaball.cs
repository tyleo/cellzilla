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
        /// <remarks>
        /// If scale is ignored, we can skip the InverseTransformPoint and just do a simple vector
        /// division of the world-space coordinates. This results in a significant speedup.
        /// </remarks>
        public sealed override float GetIntensity(Vector3 pointWorldSpaceVertex)
        {
            var entityToPointVertex = transform.InverseTransformPoint(pointWorldSpaceVertex);

            return 0.5f / entityToPointVertex.sqrMagnitude;
        }
    }
}
