using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public abstract class UVCreator :
        MonoBehaviour
    {
        public abstract Vector2[] CreateUVs(Vector3[] vertices);
    }
}
