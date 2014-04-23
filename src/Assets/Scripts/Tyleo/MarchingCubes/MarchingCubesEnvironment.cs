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

            var meshBuilder = new MeshBuilder(_mesh.vertexCount);
        }

        private static void UpdateCubes(uint frameCount, MarchingCube[, ,] cubes, List<MarchingEntity> marchingEntities, Transform environmentTransform)
        {
            foreach (var marchingEntity in marchingEntities)
            {
                var environmentToEntityVector = marchingEntity.transform.position - environmentTransform.position;
                var rotatedEnvironmentToEntityVector = environmentTransform.rotation * environmentToEntityVector;

                var scaledRotatedEnvironmentToEntityVector =
                    new Vector3(
                        rotatedEnvironmentToEntityVector.x / environmentTransform.lossyScale.x,
                        rotatedEnvironmentToEntityVector.y / environmentTransform.lossyScale.y,
                        rotatedEnvironmentToEntityVector.z / environmentTransform.lossyScale.z
                    );

                var unitIndexVector =
                    scaledRotatedEnvironmentToEntityVector +
                    new Vector3(
                        +0.5f,
                        +0.5f,
                        +0.5f
                    );

                var indexVector =
                    new Vector3(
                        unitIndexVector.x * cubes.GetLength(AxisIndexProvider.XIndex),
                        unitIndexVector.y * cubes.GetLength(AxisIndexProvider.YIndex),
                        unitIndexVector.z * cubes.GetLength(AxisIndexProvider.ZIndex)
                    );

                var i = ((int)indexVector.x).Clamp(0, cubes.GetLength(AxisIndexProvider.XIndex) - 1);
                var j = ((int)indexVector.y).Clamp(0, cubes.GetLength(AxisIndexProvider.YIndex) - 1);
                var k = ((int)indexVector.z).Clamp(0, cubes.GetLength(AxisIndexProvider.ZIndex) - 1);

                if (UpdateCubesLinearly(frameCount, cubes, marchingEntities, environmentTransform, i, j, ref k))
                {
                    UpdateCubesRecursively(frameCount, cubes, marchingEntities, environmentTransform, i, j, k);
                }
            }
        }

        private static bool UpdateCubesLinearly(uint frameCount, MarchingCube[, ,] cubes, List<MarchingEntity> marchingEntities, Transform environmentTransform, int i, int j, ref int k)
        {
            if (k < cubes.GetLength(AxisIndexProvider.ZIndex) / 2)
            {
                while (k < cubes.GetLength(AxisIndexProvider.ZIndex) && !ProcessCube(frameCount, cubes[i, j, k], marchingEntities, environmentTransform))
                {
                    ++k;
                }

                return k < cubes.GetLength(AxisIndexProvider.ZIndex);
            }
            else
            {
                while (k >= 0 && !ProcessCube(frameCount, cubes[i, j, k], marchingEntities, environmentTransform))
                {
                    --k;
                }

                return k >= 0;
            }
        }

        private static void UpdateCubesRecursively(uint frameCount, MarchingCube[, ,] cubes, List<MarchingEntity> marchingEntities, Transform environmentTransform, int i, int j, int k)
        {
            if (ProcessCube(frameCount, cubes[i + 0, j + 0, k - 1], marchingEntities, environmentTransform))
            {
                UpdateCubesRecursively(frameCount, cubes, marchingEntities, environmentTransform, i + 0, j + 0, k - 1);
            }

            if (ProcessCube(frameCount, cubes[i + 0, j + 0, k + 1], marchingEntities, environmentTransform))
            {
                UpdateCubesRecursively(frameCount, cubes, marchingEntities, environmentTransform, i + 0, j + 0, k + 1);
            }

            if (ProcessCube(frameCount, cubes[i + 0, j - 1, k + 0], marchingEntities, environmentTransform))
            {
                UpdateCubesRecursively(frameCount, cubes, marchingEntities, environmentTransform, i + 0, j - 1, k + 0);
            }

            if (ProcessCube(frameCount, cubes[i + 0, j + 1, k + 0], marchingEntities, environmentTransform))
            {
                UpdateCubesRecursively(frameCount, cubes, marchingEntities, environmentTransform, i + 0, j + 1, k + 0);
            }

            if (ProcessCube(frameCount, cubes[i - 1, j + 0, k + 0], marchingEntities, environmentTransform))
            {
                UpdateCubesRecursively(frameCount, cubes, marchingEntities, environmentTransform, i - 1, j + 0, k + 0);
            }

            if (ProcessCube(frameCount, cubes[i + 1, j + 0, k + 0], marchingEntities, environmentTransform))
            {
                UpdateCubesRecursively(frameCount, cubes, marchingEntities, environmentTransform, i + 1, j + 0, k + 0);
            }
        }

        private static bool ProcessCube(uint frameCount, MarchingCube cube, List<MarchingEntity> marchingEntities, Transform environmentTransform)
        {
            if (cube.LastFrameTouched == frameCount)
            {
                return false;
            }

            cube.ProcessPoints(frameCount, marchingEntities, environmentTransform);

            throw new System.NotImplementedException();
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
