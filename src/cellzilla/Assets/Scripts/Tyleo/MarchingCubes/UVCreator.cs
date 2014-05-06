using UnityEngine;

namespace Tyleo.MarchingCubes
{
    /// <summary>
    /// Provides methods for creating UVs from a set of vertices.
    /// </summary>
    public abstract class UVCreator :
        MonoBehaviour
    {
        /// <summary>
        /// Creates UVs from a set of vertices.
        /// </summary>
        /// <param name="vertices">
        /// The set of vertices to create UVs from.
        /// </param>
        /// <returns>
        /// The UVs created from the set of vertices.
        /// </returns>
        public abstract Vector2[] CreateUVs(Vector3[] vertices);
    }
}
