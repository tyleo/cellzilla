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
        public sealed override float GetIntensity(Vector3 entityEnvironmentSpaceVertex, Vector3 pointEnvironmentSpaceVertex)
        {
            var entityToPointVertex = pointEnvironmentSpaceVertex - entityEnvironmentSpaceVertex;

            // Dividing by lossyScale here slows everything down significantly. An alternative
            // implementation might simply provide a radius field which can be used as the dividend
            // in the division.

            // Interestingly, we can avoid this division by using a pointWorldSpaceVertex and
            // entityWorldSpaceVertex instead. Assuming we have some 8,000 points, that would
            // require 8,000 inverseTransforms instead of 8,000 * 3 divisions. Thinking now, this
            // might be cheaper.
            var lossyScale = transform.lossyScale;
            var scaledEntityToPointVertex =
                new Vector3(
                    entityToPointVertex.x / lossyScale.x,
                    entityToPointVertex.y / lossyScale.y,
                    entityToPointVertex.z / lossyScale.z
                );

            return 0.5f / scaledEntityToPointVertex.sqrMagnitude;
        }
    }
}
