using MsDebug = System.Diagnostics.Debug;

namespace Tyleo.MarchingCubes
{
    public static class CubeLatticeGenerator
    {
        public static MarchingCube[, ,] CreateCubes(int cubesAlongX, int cubesAlongY, int cubesAlongZ)
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
            return ((float)xIndex - (float)(pointsAlongX - 1) / 2.0f) / (float)pointsAlongX;
        }

        private static float GetYLocation(int pointsAlongY, int yIndex)
        {
            return ((float)yIndex - (float)(pointsAlongY - 1) / 2.0f) / (float)pointsAlongY;
        }

        private static float GetZLocation(int pointsAlongZ, int zIndex)
        {
            return ((float)zIndex - (float)(pointsAlongZ - 1) / 2.0f) / (float)pointsAlongZ;
        }
    }
}
