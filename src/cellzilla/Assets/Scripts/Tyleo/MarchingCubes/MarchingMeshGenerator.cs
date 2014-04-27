using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public static class MarchingMeshGenerator
    {
        public static MeshDataProvider GenerateMeshData(MarchingMeshGeneratorParameterPack parameters, int predictedNumberOfVertices, int predictedNumberOfTriangleIndices)
        {
            var result = new MeshDataProvider(predictedNumberOfVertices, predictedNumberOfTriangleIndices);

            UpdateCubes(parameters, result);
            return result;
        }

        private static void UpdateCubes(MarchingMeshGeneratorParameterPack parameters, MeshDataProvider meshData)
        {
            foreach (var marchingEntity in parameters.MarchingEntities)
            {
                // First we convert the center of a marching entity into an index in our cube
                // lattice.
                marchingEntity.SetEnvironmentSpaceCoordinates(parameters.CubeEnvironmentTransform);

                var unitIndexVector =
                    marchingEntity.EnvironmentSpaceCoordinates +
                    new Vector3(
                        +0.5f,
                        +0.5f,
                        +0.5f
                    );

                var indexVector =
                    new Vector3(
                        unitIndexVector.x * parameters.CubesAlongX,
                        unitIndexVector.y * parameters.CubesAlongY,
                        unitIndexVector.z * parameters.CubesAlongZ
                    );

                var i = ((int)indexVector.x).Clamp(0, parameters.CubesAlongX - 1);
                var j = ((int)indexVector.y).Clamp(0, parameters.CubesAlongY - 1);
                var k = ((int)indexVector.z).Clamp(0, parameters.CubesAlongZ - 1);

                // Next we check linearly outwards from the cube which is closest to the center of
                // our marching entity. The theory is that some mesh will probably be generated
                // around the entity. The variable, "k", changes here because we check along the
                // Z-axis.
                if (UpdateCubesLinearly(parameters, meshData, i, j, ref k))
                {
                    // If a mesh is around the entity, we check all of the cubes next to the one we
                    // found on the surface of the mesh.

                    UpdateCubesRecursively(parameters, meshData, i, j, k);
                }
            }
        }

        private static bool UpdateCubesLinearly(MarchingMeshGeneratorParameterPack parameters, MeshDataProvider meshData, int i, int j, ref int k)
        {
            // This while loop is split for a little extra performance. We start at a cube and look
            // along the Z-axis for a place to generate a mesh. If we don't find a place we return
            // false.
            if (k > parameters.CubesAlongZ / 2)
            {
                while (k < parameters.CubesAlongZ && !ProcessCube(parameters, meshData, i, j, k))
                {
                    ++k;
                }

                return k < parameters.CubesAlongZ;
            }
            else
            {
                while (k >= 0 && !ProcessCube(parameters, meshData, i, j, k))
                {
                    --k;
                }

                return k >= 0;
            }
        }

        private static void UpdateCubesRecursively(MarchingMeshGeneratorParameterPack parameters, MeshDataProvider meshData, int i, int j, int k)
        {
            // Here we just check the cubes above, below, left, right, front, back and see if they
            // also need a mesh. We make the assumption that the cube at i, j, k is already part of
            // a mesh.
            
            if (k > 0 && ProcessCube(parameters, meshData, i + 0, j + 0, k - 1))
            {
                UpdateCubesRecursively(parameters, meshData, i + 0, j + 0, k - 1);
            }

            if (k < parameters.CubesAlongZ - 1 && ProcessCube(parameters, meshData, i + 0, j + 0, k + 1))
            {
                UpdateCubesRecursively(parameters, meshData, i + 0, j + 0, k + 1);
            }

            if (j > 0 && ProcessCube(parameters, meshData, i + 0, j - 1, k + 0))
            {
                UpdateCubesRecursively(parameters, meshData, i + 0, j - 1, k + 0);
            }

            if (j < parameters.CubesAlongY - 1 && ProcessCube(parameters, meshData, i + 0, j + 1, k + 0))
            {
                UpdateCubesRecursively(parameters, meshData, i + 0, j + 1, k + 0);
            }

            if (i > 0 && ProcessCube(parameters, meshData, i - 1, j + 0, k + 0))
            {
                UpdateCubesRecursively(parameters, meshData, i - 1, j + 0, k + 0);
            }

            if (i < parameters.CubesAlongX - 1 && ProcessCube(parameters, meshData, i + 1, j + 0, k + 0))
            {
                UpdateCubesRecursively(parameters, meshData, i + 1, j + 0, k + 0);
            }
        }

        private static bool ProcessCube(MarchingMeshGeneratorParameterPack parameters, MeshDataProvider meshData, int i, int j, int k)
        {
            var cube = parameters.GetCube(i, j, k);
            if (cube.LastFrameTouched == parameters.CurrentFrameIndex)
            {
                return false;
            }

            var result = cube.Process(parameters.CurrentFrameIndex, parameters.MarchingEntities, parameters.CubeEnvironmentTransform, parameters.IntensityThreshold, meshData);

            return result;
        }
    }
}
