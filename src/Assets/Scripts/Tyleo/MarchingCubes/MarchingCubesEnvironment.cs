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

            _cubes = CreateCubes(_cubesAlongX, _cubesAlongY, _cubesAlongZ);
        }

        private static MarchingCube[, ,] CreateCubes(int cubesAlongX, int cubesAlongY, int cubesAlongZ)
        {
            MsDebug.Assert(cubesAlongX > 0);
            MsDebug.Assert(cubesAlongY > 0);
            MsDebug.Assert(cubesAlongZ > 0);

            var points = new MarchingPoint[cubesAlongX + 1, cubesAlongY + 1, cubesAlongZ + 1];

            var xEdges = new MarchingEdge[cubesAlongX + 0, cubesAlongY + 1, cubesAlongZ + 1];
            var yEdges = new MarchingEdge[cubesAlongX + 1, cubesAlongY + 0, cubesAlongZ + 1];
            var zEdges = new MarchingEdge[cubesAlongX + 1, cubesAlongY + 1, cubesAlongZ + 0];

            var cubes = new MarchingCube[cubesAlongX, cubesAlongY, cubesAlongZ];

            // We create a single vertex first. This means that if we continue to create adjacent
            // vertices we will ALWAYS be creating new edges.
            CreateInitialXYZVertex(points);

            // By only creating the edges we can guarantee that exactly one edge will be created
            // with each new adjacent vertex. These three methods handle all the cases where exactly
            // one edge can be created per vertex.
            CreateInitialXYEdge(points, zEdges);
            CreateInitialXZEdge(points, yEdges);
            CreateInitialYZEdge(points, xEdges);

            // Like the previous methods, these methods guarantee a constant amount of edges will be
            // created, in this case, 2. By creating only the faces we get 2 edges per vertex.
            CreateInitialXFace(points, yEdges, zEdges);
            CreateInitialYFace(points, xEdges, zEdges);
            CreateInitialZFace(points, xEdges, yEdges);

            // Since we have three of the faces around the corner of the cube, we can guarantee that
            // exactly one cube and three edges will be created per cycle in this method. All of our
            // cubes can be created at once this way.
            CreateXYZCubes(points, cubes, xEdges, yEdges, zEdges);

            return cubes;
        }

        private static void CreateInitialXYZVertex(MarchingPoint[, ,] points)
        {
            const int i = 0;
            const int j = 0;
            const int k = 0;

            points[i, j, k] = 
                new MarchingPoint(
                    GetXLocation(points.GetLength(AxisIndexProvider.XIndex), i),
                    GetYLocation(points.GetLength(AxisIndexProvider.YIndex), j),
                    GetZLocation(points.GetLength(AxisIndexProvider.ZIndex), k)
                );
        }

        private static void CreateInitialXYEdge(MarchingPoint[, ,] points, MarchingEdge[, ,] zEdges)
        {
            const int i = 0;
            const int j = 0;

            for (int k = 1; k < points.GetLength(AxisIndexProvider.ZIndex); ++k)
            {
                points[i, j, k] =
                    new MarchingPoint(
                        GetXLocation(points.GetLength(AxisIndexProvider.XIndex), i),
                        GetYLocation(points.GetLength(AxisIndexProvider.YIndex), j),
                        GetZLocation(points.GetLength(AxisIndexProvider.ZIndex), k)
                    );

                zEdges[i + 0, j + 0, k - 1] =
                    new MarchingEdge(
                        points[i + 0, j + 0, k - 1],
                        points[i + 0, j + 0, k + 0]
                    );
            }
        }

        private static void CreateInitialXZEdge(MarchingPoint[, ,] points, MarchingEdge[, ,] yEdges)
        {
            const int i = 0;
            const int k = 0;

            for (int j = 1; j < points.GetLength(AxisIndexProvider.YIndex); ++j)
            {
                points[i, j, k] =
                    new MarchingPoint(
                        GetXLocation(points.GetLength(AxisIndexProvider.XIndex), i),
                        GetYLocation(points.GetLength(AxisIndexProvider.YIndex), j),
                        GetZLocation(points.GetLength(AxisIndexProvider.ZIndex), k)
                    );

                yEdges[i + 0, j - 1, k + 0] =
                    new MarchingEdge(
                        points[i + 0, j - 1, k + 0],
                        points[i + 0, j + 0, k + 0]
                    );
            }
        }

        private static void CreateInitialYZEdge(MarchingPoint[, ,] points, MarchingEdge[, ,] xEdges)
        {
            const int j = 0;
            const int k = 0;

            for (int i = 1; i < points.GetLength(AxisIndexProvider.XIndex); ++i)
            {
                points[i, j, k] =
                    new MarchingPoint(
                        GetXLocation(points.GetLength(AxisIndexProvider.XIndex), i),
                        GetYLocation(points.GetLength(AxisIndexProvider.YIndex), j),
                        GetZLocation(points.GetLength(AxisIndexProvider.ZIndex), k)
                    );

                xEdges[i - 1, j + 0, k + 0] =
                    new MarchingEdge(
                        points[i - 1, j + 0, k + 0],
                        points[i + 0, j + 0, k + 0]
                    );
            }
        }

        private static void CreateInitialXFace(MarchingPoint[, ,] points, MarchingEdge[, ,] yEdges, MarchingEdge[, ,] zEdges)
        {
            const int i = 0;

            for (int j = 1; j < points.GetLength(AxisIndexProvider.YIndex); ++j)
            {
                for (int k = 1; k < points.GetLength(AxisIndexProvider.ZIndex); ++k)
                {
                    points[i, j, k] =
                        new MarchingPoint(
                            GetXLocation(points.GetLength(AxisIndexProvider.XIndex), i),
                            GetYLocation(points.GetLength(AxisIndexProvider.YIndex), j),
                            GetZLocation(points.GetLength(AxisIndexProvider.ZIndex), k)
                        );

                    yEdges[i + 0, j - 1, k + 0] =
                        new MarchingEdge(
                            points[i + 0, j - 1, k + 0],
                            points[i + 0, j + 0, k + 0]
                        );
                    zEdges[i + 0, j + 0, k - 1] =
                        new MarchingEdge(
                            points[i + 0, j + 0, k - 1],
                            points[i + 0, j + 0, k + 0]
                        );
                }
            }
        }

        private static void CreateInitialYFace(MarchingPoint[, ,] points, MarchingEdge[, ,] xEdges, MarchingEdge[, ,] zEdges)
        {
            const int j = 0;

            for (int i = 1; i < points.GetLength(AxisIndexProvider.XIndex); ++i)
            {
                for (int k = 1; k < points.GetLength(AxisIndexProvider.ZIndex); ++k)
                {
                    points[i, j, k] =
                        new MarchingPoint(
                            GetXLocation(points.GetLength(AxisIndexProvider.XIndex), i),
                            GetYLocation(points.GetLength(AxisIndexProvider.YIndex), j),
                            GetZLocation(points.GetLength(AxisIndexProvider.ZIndex), k)
                        );

                    xEdges[i - 1, j + 0, k + 0] =
                        new MarchingEdge(
                            points[i - 1, j + 0, k + 0],
                            points[i + 0, j + 0, k + 0]
                        );
                    zEdges[i + 0, j + 0, k - 1] =
                        new MarchingEdge(
                            points[i + 0, j + 0, k - 1],
                            points[i + 0, j + 0, k + 0]
                        );
                }
            }
        }

        private static void CreateInitialZFace(MarchingPoint[, ,] points, MarchingEdge[, ,] xEdges, MarchingEdge[, ,] yEdges)
        {
            const int k = 0;

            for (int i = 1; i < points.GetLength(AxisIndexProvider.XIndex); ++i)
            {
                for (int j = 1; j < points.GetLength(AxisIndexProvider.YIndex); ++j)
                {
                    points[i, j, k] =
                        new MarchingPoint(
                            GetXLocation(points.GetLength(AxisIndexProvider.XIndex), i),
                            GetYLocation(points.GetLength(AxisIndexProvider.YIndex), j),
                            GetZLocation(points.GetLength(AxisIndexProvider.ZIndex), k)
                        );

                    xEdges[i - 1, j + 0, k + 0] =
                        new MarchingEdge(
                            points[i - 1, j + 0, k + 0],
                            points[i + 0, j + 0, k + 0]
                        );
                    yEdges[i + 0, j - 1, k + 0] =
                        new MarchingEdge(
                            points[i + 0, j - 1, k + 0],
                            points[i + 0, j + 0, k + 0]
                        );
                }
            }
        }

        private static void CreateXYZCubes(MarchingPoint[, ,] points, MarchingCube[, ,] cubes, MarchingEdge[, ,] xEdges, MarchingEdge[, ,] yEdges, MarchingEdge[, ,] zEdges)
        {
            for (int i = 1; i < points.GetLength(AxisIndexProvider.XIndex); ++i)
            {
                for (int j = 1; j < points.GetLength(AxisIndexProvider.YIndex); ++j)
                {
                    for (int k = 1; k < points.GetLength(AxisIndexProvider.ZIndex); ++k)
                    {
                        points[i, j, k] =
                            new MarchingPoint(
                                GetXLocation(points.GetLength(AxisIndexProvider.XIndex), i),
                                GetYLocation(points.GetLength(AxisIndexProvider.YIndex), j),
                                GetZLocation(points.GetLength(AxisIndexProvider.ZIndex), k)
                            );

                        xEdges[i - 1, j + 0, k + 0] =
                            new MarchingEdge(
                                points[i - 1, j + 0, k + 0],
                                points[i + 0, j + 0, k + 0]
                            );
                        yEdges[i + 0, j - 1, k + 0] =
                            new MarchingEdge(
                                points[i + 0, j - 1, k + 0],
                                points[i + 0, j + 0, k + 0]
                            );
                        zEdges[i + 0, j + 0, k - 1] =
                            new MarchingEdge(
                                points[i + 0, j + 0, k - 1],
                                points[i + 0, j + 0, k + 0]
                            );

                        cubes[i - 1, j - 1, k - 1] =
                            new MarchingCube(
                                points[i - 1, j - 1, k - 1],
                                points[i - 1, j - 1, k + 0],
                                points[i - 1, j + 0, k - 1],
                                points[i - 1, j + 0, k + 0],
                                points[i + 0, j - 1, k - 1],
                                points[i + 0, j - 1, k + 0],
                                points[i + 0, j + 0, k - 1],
                                points[i + 0, j + 0, k + 0],

                                xEdges[i - 1, j - 1, k - 1],
                                xEdges[i - 1, j - 1, k + 0],
                                xEdges[i - 1, j + 0, k - 1],
                                xEdges[i - 1, j + 0, k + 0],

                                yEdges[i - 1, j - 1, k - 1],
                                yEdges[i + 0, j - 1, k - 1],
                                yEdges[i - 1, j - 1, k + 0],
                                yEdges[i + 0, j - 1, k + 0],

                                zEdges[i - 1, j - 1, k - 1],
                                zEdges[i - 1, j + 0, k - 1],
                                zEdges[i + 0, j - 1, k - 1],
                                zEdges[i + 0, j + 0, k - 1]
                            );
                    }
                }
            }
        }

        private static float GetXLocation(int pointsAlongX, int xIndex)
        {
            return (float)xIndex - (float)(pointsAlongX - 1) / 2.0f;
        }

        private static float GetYLocation(int pointsAlongY, int yIndex)
        {
            return (float)yIndex - (float)(pointsAlongY - 1) / 2.0f;
        }

        private static float GetZLocation(int pointsAlongZ, int zIndex)
        {
            return (float)zIndex - (float)(pointsAlongZ - 1) / 2.0f;
        }
    }
}
