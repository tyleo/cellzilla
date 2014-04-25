using MsDebug = System.Diagnostics.Debug;
using System.Collections.Generic;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingCubesEnvironment :
        MonoBehaviour
    {
        // The number of vertices and triangle indices in the mesh must be predicted because we can
        // only get memory on the graphics card once. Therefore, we want to take a little more than
        // we need. By tweaking the values below we can estimate a little more or less.
        private const float VERTEX_ESTIMATION_CONSTANT = 5.0f;
        private const float TRIANGLE_ESTIMATION_CONSTANT = 0.5f;

        [SerializeField]
        private int _predictedNumberOfVertices = -1;
        [SerializeField]
        private int _predictedNumberOfTriangleIndices = -1;
        [SerializeField]
        private int _cubesAlongX = 40;
        [SerializeField]
        private int _cubesAlongY = 40;
        [SerializeField]
        private int _cubesAlongZ = 40;
        [SerializeField]
        private float _threshold = 2.5f;
        [SerializeField]
        private List<MarchingEntity> _marchingEntities = new List<MarchingEntity>();

        private Mesh _mesh;
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

            var meshDataProvider = MarchingMeshGenerator.GenerateMeshData(marchingMeshGeneratorParameterPack, _predictedNumberOfVertices, _predictedNumberOfTriangleIndices);

            _mesh.vertices = meshDataProvider.GetVetrices();
            _mesh.uv = _uvCreator == null ? new Vector2[_mesh.vertices.Length] : _uvCreator.CreateUVs(_mesh.vertices);
            _mesh.triangles = meshDataProvider.GetTriangles();
            _mesh.normals = meshDataProvider.GetNormals();
        }

        private void Start()
        {
            _mesh = new Mesh()
            {
            };
            _mesh.MarkDynamic();
            GetComponent<MeshFilter>().mesh = _mesh;

            _uvCreator = GetComponent<UVCreator>();

            _cubes = CubeLatticeGenerator.CreateCubes(_cubesAlongX, _cubesAlongY, _cubesAlongZ);

            if (_predictedNumberOfVertices == -1)
            {
                _predictedNumberOfVertices = (int)(_cubesAlongX * _cubesAlongY * _cubesAlongZ / VERTEX_ESTIMATION_CONSTANT);
            }

            if (_predictedNumberOfTriangleIndices == -1)
            {
                var estimationValue = (int)(_cubesAlongX * _cubesAlongY * _cubesAlongZ / TRIANGLE_ESTIMATION_CONSTANT);
                _predictedNumberOfTriangleIndices = estimationValue - estimationValue % 3;
            }
        }
    }
}
