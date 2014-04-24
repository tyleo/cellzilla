using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingMetaballUVCreator :
        UVCreator
    {
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
