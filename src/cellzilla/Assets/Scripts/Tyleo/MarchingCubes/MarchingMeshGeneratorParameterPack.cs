using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    /// <summary>
    /// Packages the parameters used by the MarchingMeshGenerator's GenerateMeshData method as a
    /// single unit.
    /// </summary>
    /// <remarks>
    /// These parameters are packaged because GenerateMeshData makes a lot of method calls which use
    /// these same parameters. Keeping them all together is a significant convenience.
    /// </remarks>
    public sealed class MarchingMeshGeneratorParameterPack
    {
        private readonly uint _currentFrameIndex;
        private readonly MarchingCube[, ,] _cubes;
        private readonly IEnumerable<MarchingEntity> _marchingEntities;
        private readonly Transform _cubeEnvironmentTransform;
        private readonly float _intensityThreshold;

        /// <summary>
        /// The index of the current frame.
        /// </summary>
        public uint CurrentFrameIndex { get { return _currentFrameIndex; } }
        /// <summary>
        /// The marching entities which participate in determining the geometry of the mesh.
        /// </summary>
        public IEnumerable<MarchingEntity> MarchingEntities { get { return _marchingEntities; } }
        /// <summary>
        /// A transform used for convert world-space vertices into local-space vertices of the
        /// MarchingCubesEnvironment and vice versa.
        /// </summary>
        public Transform CubeEnvironmentTransform { get { return _cubeEnvironmentTransform; } }
        /// <summary>
        /// The number of MarchingCubes along the X-axis.
        /// </summary>
        public int CubesAlongX { get { return _cubes.GetLength(AxisIndexProvider.XIndex); } }
        /// <summary>
        /// The number of MarchingCubes along the Y-axis.
        /// </summary>
        public int CubesAlongY { get { return _cubes.GetLength(AxisIndexProvider.YIndex); } }
        /// <summary>
        /// The number of MarchingCubes along the Z-axis.
        /// </summary>
        public int CubesAlongZ { get { return _cubes.GetLength(AxisIndexProvider.ZIndex); } }
        /// <summary>
        /// The threshold value which determines where the surface of the mesh will be generated.
        /// Vertices below the threshold will be considered within the mesh, points above, outside.
        /// </summary>
        public float IntensityThreshold { get { return _intensityThreshold; } }

        /// <summary>
        /// Gets a cube by index.
        /// </summary>
        /// <param name="xIndex">
        /// The X-index of the cube.
        /// </param>
        /// <param name="yIndex">
        /// The Y-index of the cube.
        /// </param>
        /// <param name="zIndex">
        /// The Z-index of the cube.
        /// </param>
        /// <returns>
        /// The cube specified by the indices.
        /// </returns>
        public MarchingCube GetCube(int xIndex, int yIndex, int zIndex)
        {
            return _cubes[xIndex, yIndex, zIndex];
        }

        /// <summary>
        /// Creates a new MarchingMeshGeneratorParameterPack.
        /// </summary>
        /// <param name="currentFrameIndex">
        /// The index of the current frame.
        /// </param>
        /// <param name="cubes">
        /// The cube lattice of the MarchingCubesEnvironment.
        /// </param>
        /// <param name="marchingEntities">
        /// The marching entities which participate in determining the geometry of the mesh.
        /// </param>
        /// <param name="cubeEnvironmentTransform">
        /// A transform used for convert world-space vertices into local-space vertices of the
        /// MarchingCubesEnvironment and vice versa.
        /// </param>
        /// <param name="intensityThreshold">
        /// The threshold value which determines where the surface of the mesh will be generated.
        /// Vertices below the threshold will be considered within the mesh, points above, outside.
        /// </param>
        public MarchingMeshGeneratorParameterPack(uint currentFrameIndex, MarchingCube[, ,] cubes, IEnumerable<MarchingEntity> marchingEntities, Transform cubeEnvironmentTransform, float intensityThreshold)
        {
            _currentFrameIndex = currentFrameIndex;
            _cubes = cubes;
            _marchingEntities = marchingEntities;
            _cubeEnvironmentTransform = cubeEnvironmentTransform;
            _intensityThreshold = intensityThreshold;
        }
    }
}
