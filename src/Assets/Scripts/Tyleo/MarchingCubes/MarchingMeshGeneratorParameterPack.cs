using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingMeshGeneratorParameterPack
    {
        private readonly uint _currentFrameIndex;
        private readonly MarchingCube[, ,] _cubes;
        private readonly IEnumerable<MarchingEntityEnvironmentPositionPair> _marchingEntitiesWithEnvironmentPositions;
        private readonly float _intensityThreshold;

        public uint CurrentFrameIndex { get { return _currentFrameIndex; } }
        public IEnumerable<MarchingEntityEnvironmentPositionPair> MarchingEntitiesWithEnvironmentPositions { get { return _marchingEntitiesWithEnvironmentPositions; } }
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
            _marchingEntitiesWithEnvironmentPositions =
                (
                    from marchingEntity in marchingEntities
                    select new MarchingEntityEnvironmentPositionPair(marchingEntity, cubeEnvironmentTransform)
                ).ToArray();
            _intensityThreshold = intensityThreshold;
        }
    }
}
