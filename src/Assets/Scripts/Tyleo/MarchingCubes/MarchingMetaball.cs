using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingMetaball :
        MarchingEntity
    {
        private float _radius;

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
            var metaballToWorldSpaceVertex = worldSpaceVertex - transform.position;

            return _radius / metaballToWorldSpaceVertex.sqrMagnitude;
        }

        private void Start()
        {
            // The circumference is the average of the scale in each direction; the radius is half
            // of that.
            _radius =
                (
                    transform.localScale.x +
                    transform.localScale.y +
                    transform.localScale.z
                ) / 6.0f;
        }
    }
}
