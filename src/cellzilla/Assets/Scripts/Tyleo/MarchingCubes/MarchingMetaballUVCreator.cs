using UnityEngine;

namespace Tyleo.MarchingCubes
{
    /// <summary>
    /// Provides methods for creating UVs for metaballs.
    /// </summary>
    /// <remarks>
    /// I'm not sure this class works correctly.
    /// </remarks>
    public sealed class MarchingMetaballUVCreator :
        UVCreator
    {
        /// <summary>
        /// Creates UVs for a set of vertices as though they were vertices of a metaball.
        /// </summary>
        /// <param name="vertices">
        /// The vertices to create UVs for.
        /// </param>
        /// <returns>
        /// UVs for a set of vertices as though they were vertices of a metaball.
        /// </returns>
        public sealed override Vector2[] CreateUVs(Vector3[] vertices)
        {
            var result = new Vector2[vertices.Length];

            for (int i = 0; i < result.Length; ++i)
            {
                var thisVertex = vertices[i];

                result[i] =
                    new Vector2(
                        (thisVertex.x + 1) / 2,
                        (thisVertex.y + 1) / 2
                    );
            }

            return result;
        }
    }
}
