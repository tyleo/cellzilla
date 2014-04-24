using MsDebug = System.Diagnostics.Debug;
using System.Collections.Generic;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingCubesEnvironment :
        MonoBehaviour
    {
        [SerializeField]
        private int _cubesAlongX = 20;
        [SerializeField]
        private int _cubesAlongY = 20;
        [SerializeField]
        private int _cubesAlongZ = 20;
        [SerializeField]
        private float _threshold = 8.0f;
        [SerializeField]
        private List<MarchingEntity> _marchingEntities = new List<MarchingEntity>();

        private Mesh _mesh;

        private MarchingCube[, ,] _cubes;

        private uint _frameCount = 0;

        private void Update()
        {
            unchecked { _frameCount++; }

            var marchingMeshGeneratorParameterPack =
                new MarchingMeshGeneratorParameterPack(
                    _frameCount,
                    _cubes,
                    _marchingEntities,
                    transform,
                    _threshold
                );

            var meshDataProvider = MarchingMeshGenerator.GenerateMeshData(marchingMeshGeneratorParameterPack, _mesh.vertexCount, _mesh.triangles.Length);

            _mesh.vertices = meshDataProvider.GetVetrices();
            _mesh.uv = meshDataProvider.GetUV();
            _mesh.triangles = meshDataProvider.GetTriangles();
            _mesh.normals = meshDataProvider.GetNormals();
        }

        private void Start()
        {
            var meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh = new Mesh();
            _mesh = meshFilter.mesh;

            _cubes = CubeLatticeGenerator.CreateCubes(_cubesAlongX, _cubesAlongY, _cubesAlongZ);
        }
    }
}
