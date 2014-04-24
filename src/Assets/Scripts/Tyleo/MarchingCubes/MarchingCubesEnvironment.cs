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
        private float _threshold = 2.5f;
        [SerializeField]
        private List<MarchingEntity> _marchingEntities = new List<MarchingEntity>();

        private MeshFilter _meshFilter;
        private UVCreator _uvCreator;

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

            var meshDataProvider = MarchingMeshGenerator.GenerateMeshData(marchingMeshGeneratorParameterPack, _meshFilter.mesh.vertexCount, _meshFilter.mesh.triangles.Length);

            var vertices = meshDataProvider.GetVetrices();

            _meshFilter.mesh = new Mesh() {
                vertices = vertices,
                uv = _uvCreator == null ? new Vector2[vertices.Length] : _uvCreator.CreateUVs(vertices),
                triangles = meshDataProvider.GetTriangles(),
                normals = meshDataProvider.GetNormals()
            };
        }

        private void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();

            _uvCreator = GetComponent<UVCreator>();

            _cubes = CubeLatticeGenerator.CreateCubes(_cubesAlongX, _cubesAlongY, _cubesAlongZ);
        }
    }
}
