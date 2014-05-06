using MsDebug = System.Diagnostics.Debug;
using System.Collections.Generic;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    /// <summary>
    /// An environment in which a mesh can be generated from the marching cubes algorithm.
    /// </summary>
    public sealed class MarchingCubesEnvironment :
        MonoBehaviour
    {
        // The number of vertices and triangle indices in the mesh must be predicted because we can
        // only get memory on the graphics card once. Therefore, we want to take a little more than
        // we need. By tweaking the values below we can estimate a little more or less.
        private const float VERTEX_ESTIMATION_CONSTANT = 1.0f;
        private const float TRIANGLE_ESTIMATION_CONSTANT = 0.5f;

        /// <summary>
        /// The maximum number of vertices the generated mesh can have. Use -1 if you want the
        /// algorithm to estimate this value.
        /// </summary>
        [SerializeField]
        private int _predictedNumberOfVertices = -1;
        /// <summary>
        /// The maximum number of triangle indices the generated mesh can have. Use -1 if you want
        /// the algorithm to estimate this value.
        /// </summary>
        [SerializeField]
        private int _predictedNumberOfTriangleIndices = -1;
        /// <summary>
        /// The number of MarchingCubes along the X-axis.
        /// </summary>
        [SerializeField]
        private int _cubesAlongX = 40;
        /// <summary>
        /// The number of MarchingCubes along the Y-axis.
        /// </summary>
        [SerializeField]
        private int _cubesAlongY = 40;
        /// <summary>
        /// The number of MarchingCubes along the Z-axis.
        /// </summary>
        [SerializeField]
        private int _cubesAlongZ = 40;
        /// <summary>
        /// The threshold value which determines where the surface of the mesh will be generated.
        /// Vertices below the threshold will be considered within the mesh, points above, outside.
        /// </summary>
        [SerializeField]
        private float _threshold = 2.5f;
        /// <summary>
        /// The marching entities which participate in determining the geometry of the mesh.
        /// </summary>
        [SerializeField]
        private List<MarchingEntity> _marchingEntities = new List<MarchingEntity>();

        private Mesh _mesh;
        private UVCreator _uvCreator;

        private MarchingCube[, ,] _cubes;

        private uint _frameCount = 0;

        /// <summary>
        /// Runs a single iteration of the marching cubes algorithm. Generates the mesh for this
        /// frame.
        /// </summary>
        private void Update()
        {
            // The frame count should roll over when it passes its max value. Thats why this
            // increment is unchecked.
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

            meshDataProvider.UpdateMesh(_mesh, _uvCreator);
        }

        private void Start()
        {
            _mesh = new Mesh();
            _mesh.MarkDynamic();
            GetComponent<MeshFilter>().mesh = _mesh;

            _uvCreator = GetComponent<UVCreator>();

            _cubes = CubeLatticeGenerator.CreateCubes(_cubesAlongX, _cubesAlongY, _cubesAlongZ);

            // The equations below estimate the number of vertices and triangle indices if no
            // prediction was provided by the user. These estimations are not very good and an
            // underestimate will cripple performance.
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
