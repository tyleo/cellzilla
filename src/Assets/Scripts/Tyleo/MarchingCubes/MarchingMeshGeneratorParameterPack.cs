using System.Collections.Generic;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingMeshGeneratorParameterPack
    {
        private readonly uint _currentFrameIndex;
        private readonly MarchingCube[, ,] _cubes;
        private readonly IEnumerable<MarchingEntity> _marchingEntities;
        private readonly Transform _cubeEnvironmentTransform;
        private readonly float _intensityThreshold;

        public uint CurrentFrameIndex { get { return _currentFrameIndex; } }
        public IEnumerable<MarchingEntity> MarchingEntities { get { return _marchingEntities; } }
        public Transform CubeEnvironmentTransform { get { return _cubeEnvironmentTransform; } }
        public int CubesAlongX { get { return _cubes.GetLength(AxisIndexProvider.XIndex); } }
        public int CubesAlongY { get { return _cubes.GetLength(AxisIndexProvider.YIndex); } }
        public int CubesAlongZ { get { return _cubes.GetLength(AxisIndexProvider.ZIndex); } }
        public float IntensityThreshold { get { return _intensityThreshold; } }

        public MarchingCube GetCube(int xIndex, int yIndex, int zIndex)
        {
            return _cubes[xIndex, yIndex, zIndex];
        }

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
