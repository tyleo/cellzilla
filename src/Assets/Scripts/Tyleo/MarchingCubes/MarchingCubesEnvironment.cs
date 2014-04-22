using MsDebug = System.Diagnostics.Debug;
using System.Collections.Generic;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingCubesEnvironment :
        MonoBehaviour
    {
        private const int X_INDEX = 0;
        private const int Y_INDEX = 1;
        private const int Z_INDEX = 2;

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

        private MarchingCube[, ,] _cubes;

        private void Start()
        {
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

            CreateInitialXYZVertex(points);

            CreateInitialXYEdge(points, zEdges);
            CreateInitialXZEdge(points, yEdges);
            CreateInitialYZEdge(points, xEdges);

            CreateInitialXFace(points, yEdges, zEdges);
            CreateInitialYFace(points, xEdges, zEdges);
            CreateInitialZFace(points, xEdges, yEdges);

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
                    GetXLocation(points.GetLength(X_INDEX), i),
                    GetYLocation(points.GetLength(Y_INDEX), j),
                    GetZLocation(points.GetLength(Z_INDEX), k)
                );
        }

        private static void CreateInitialXYEdge(MarchingPoint[, ,] points, MarchingEdge[, ,] zEdges)
        {
            const int i = 0;
            const int j = 0;

            for (int k = 1; k < points.GetLength(Z_INDEX); ++k)
            {
                points[i, j, k] =
                    new MarchingPoint(
                        GetXLocation(points.GetLength(X_INDEX), i),
                        GetYLocation(points.GetLength(Y_INDEX), j),
                        GetZLocation(points.GetLength(Z_INDEX), k)
                    );

                zEdges[i + 0, j + 0, k - 1] = null;
            }
        }

        private static void CreateInitialXZEdge(MarchingPoint[, ,] points, MarchingEdge[, ,] yEdges)
        {
            const int i = 0;
            const int k = 0;

            for (int j = 1; j < points.GetLength(Y_INDEX); ++j)
            {
                points[i, j, k] =
                    new MarchingPoint(
                        GetXLocation(points.GetLength(X_INDEX), i),
                        GetYLocation(points.GetLength(Y_INDEX), j),
                        GetZLocation(points.GetLength(Z_INDEX), k)
                    );

                yEdges[i + 0, j - 1, k + 0] = null;
            }
        }

        private static void CreateInitialYZEdge(MarchingPoint[, ,] points, MarchingEdge[, ,] xEdges)
        {
            const int j = 0;
            const int k = 0;

            for (int i = 1; i < points.GetLength(X_INDEX); ++i)
            {
                points[i, j, k] =
                    new MarchingPoint(
                        GetXLocation(points.GetLength(X_INDEX), i),
                        GetYLocation(points.GetLength(Y_INDEX), j),
                        GetZLocation(points.GetLength(Z_INDEX), k)
                    );

                xEdges[i - 1, j + 0, k + 0] = null;
            }
        }

        private static void CreateInitialXFace(MarchingPoint[, ,] points, MarchingEdge[, ,] yEdges, MarchingEdge[, ,] zEdges)
        {
            const int i = 0;

            for (int j = 1; j < points.GetLength(Y_INDEX); ++j)
            {
                for (int k = 1; k < points.GetLength(Z_INDEX); ++k)
                {
                    points[i, j, k] =
                        new MarchingPoint(
                            GetXLocation(points.GetLength(X_INDEX), i),
                            GetYLocation(points.GetLength(Y_INDEX), j),
                            GetZLocation(points.GetLength(Z_INDEX), k)
                        );

                    yEdges[i + 0, j - 1, k + 0] = null;
                    zEdges[i + 0, j + 0, k - 1] = null;
                }
            }
        }

        private static void CreateInitialYFace(MarchingPoint[, ,] points, MarchingEdge[, ,] xEdges, MarchingEdge[, ,] zEdges)
        {
            const int j = 0;

            for (int i = 1; i < points.GetLength(X_INDEX); ++i)
            {
                for (int k = 1; k < points.GetLength(Z_INDEX); ++k)
                {
                    points[i, j, k] =
                        new MarchingPoint(
                            GetXLocation(points.GetLength(X_INDEX), i),
                            GetYLocation(points.GetLength(Y_INDEX), j),
                            GetZLocation(points.GetLength(Z_INDEX), k)
                        );

                    xEdges[i - 1, j + 0, k + 0] = null;
                    zEdges[i + 0, j + 0, k - 1] = null;
                }
            }
        }

        private static void CreateInitialZFace(MarchingPoint[, ,] points, MarchingEdge[, ,] xEdges, MarchingEdge[, ,] yEdges)
        {
            const int k = 0;

            for (int i = 1; i < points.GetLength(X_INDEX); ++i)
            {
                for (int j = 1; j < points.GetLength(Y_INDEX); ++j)
                {
                    points[i, j, k] =
                        new MarchingPoint(
                            GetXLocation(points.GetLength(X_INDEX), i),
                            GetYLocation(points.GetLength(Y_INDEX), j),
                            GetZLocation(points.GetLength(Z_INDEX), k)
                        );

                    xEdges[i - 1, j + 0, k + 0] = null;
                    yEdges[i + 0, j - 1, k + 0] = null;
                }
            }
        }

        private static void CreateXYZCubes(MarchingPoint[, ,] points, MarchingCube[, ,] cubes, MarchingEdge[, ,] xEdges, MarchingEdge[, ,] yEdges, MarchingEdge[, ,] zEdges)
        {
            for (int i = 1; i < points.GetLength(X_INDEX); ++i)
            {
                for (int j = 1; j < points.GetLength(Y_INDEX); ++j)
                {
                    for (int k = 1; k < points.GetLength(Z_INDEX); ++k)
                    {
                        points[i, j, k] =
                            new MarchingPoint(
                                GetXLocation(points.GetLength(X_INDEX), i),
                                GetYLocation(points.GetLength(Y_INDEX), j),
                                GetZLocation(points.GetLength(Z_INDEX), k)
                            );

                        xEdges[i - 1, j + 0, k + 0] = null;
                        yEdges[i + 0, j - 1, k + 0] = null;
                        zEdges[i + 0, j + 0, k - 1] = null;

                        cubes[i - 1, j - 1, k - 1] = null;
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
