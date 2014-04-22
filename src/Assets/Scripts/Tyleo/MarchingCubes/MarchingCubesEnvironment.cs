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

        private MarchingCube[, ,] _cubes;

        private void Start()
        {
            var points = CreatePoints(_cubesAlongX, _cubesAlongY, _cubesAlongZ);
            var edgeDictionary = CreateEdges(points);
            _cubes = CreateCubes(points, null);
        }

        private static MarchingPoint[, ,] CreatePoints(int cubesAlongX, int cubesAlongY, int cubesAlongZ)
        {
            MsDebug.Assert(cubesAlongX > 0);
            MsDebug.Assert(cubesAlongY > 0);
            MsDebug.Assert(cubesAlongZ > 0);

            var result = new MarchingPoint[cubesAlongX + 1, cubesAlongY + 1, cubesAlongZ + 1];

            for (int i = 0; i < result.GetLength(0); ++i )
            {
                for (int j = 0; j < result.GetLength(1); ++j)
                {
                    for (int k = 0; k < result.GetLength(2); ++k)
                    {
                        result[i, j, k] = new MarchingPoint(
                            (float)i - (float)cubesAlongX / 2.0f,
                            (float)j - (float)cubesAlongY / 2.0f,
                            (float)k - (float)cubesAlongZ / 2.0f
                        );
                    }
                }
            }

            return result;
        }

        private static Dictionary<MarchingPointPair, MarchingEdge> CreateEdges(MarchingPoint[,,] marchingPoints)
        {
            var result = new Dictionary<MarchingPointPair, MarchingEdge>();

            // We add all of the edges except those located on boundaries. We do this to simplify
            // the logic by avoiding complicated if statements in our loop.
            AddMostEdgesToDictionary(marchingPoints, result);

            // At this point everything is in our dictionary except the edges on three faces of the
            // lattice and the edges on the edges of the lattice.
            AddFaceBoundryXEdgesToDictionary(marchingPoints, result);
            AddFaceBoundryYEdgesToDictionary(marchingPoints, result);
            AddFaceBoundryZEdgesToDictionary(marchingPoints, result);

            // At this point we just need to add the edges of the lattice.
            AddEdgeBoundryXEdgesToDictionary(marchingPoints, result);
            AddEdgeBoundryYEdgesToDictionary(marchingPoints, result);
            AddEdgeBoundryZEdgesToDictionary(marchingPoints, result);

            return result;
        }

        private static void AddMostEdgesToDictionary(MarchingPoint[, ,] marchingPoints, Dictionary<MarchingPointPair, MarchingEdge> edgeDictionary)
        {
            for (int i = 0; i < marchingPoints.GetLength(0) - 1; ++i)
            {
                for (int j = 0; j < marchingPoints.GetLength(1) - 1; ++j)
                {
                    for (int k = 0; k < marchingPoints.GetLength(2) - 1; ++k)
                    {
                        var currentStartPoint = marchingPoints[i, j, k];
                        MarchingPoint currentEndPoint;
                        
                        currentEndPoint = marchingPoints[i + 0, j + 0, k + 1];
                        edgeDictionary.Add(new MarchingPointPair(currentStartPoint, currentEndPoint), new MarchingEdge(currentStartPoint, currentEndPoint));

                        currentEndPoint = marchingPoints[i + 0, j + 1, k + 0];
                        edgeDictionary.Add(new MarchingPointPair(currentStartPoint, currentEndPoint), new MarchingEdge(currentStartPoint, currentEndPoint));

                        currentEndPoint = marchingPoints[i + 1, j + 0, k + 0];
                        edgeDictionary.Add(new MarchingPointPair(currentStartPoint, currentEndPoint), new MarchingEdge(currentStartPoint, currentEndPoint));
                    }
                }
            }
        }

        private static void AddFaceBoundryXEdgesToDictionary(MarchingPoint[, ,] marchingPoints, Dictionary<MarchingPointPair, MarchingEdge> edgeDictionary)
        {
            var i = marchingPoints.GetLength(0) - 1;

            for (int j = 0; j < marchingPoints.GetLength(1) - 1; ++j)
            {
                for (int k = 0; k < marchingPoints.GetLength(2) - 1; ++k)
                {
                    var currentStartPoint = marchingPoints[i, j, k];
                    MarchingPoint currentEndPoint;

                    currentEndPoint = marchingPoints[i + 0, j + 0, k + 1];
                    edgeDictionary.Add(new MarchingPointPair(currentStartPoint, currentEndPoint), new MarchingEdge(currentStartPoint, currentEndPoint));

                    currentEndPoint = marchingPoints[i + 0, j + 1, k + 0];
                    edgeDictionary.Add(new MarchingPointPair(currentStartPoint, currentEndPoint), new MarchingEdge(currentStartPoint, currentEndPoint));
                }
            }
        }

        private static void AddFaceBoundryYEdgesToDictionary(MarchingPoint[, ,] marchingPoints, Dictionary<MarchingPointPair, MarchingEdge> edgeDictionary)
        {
            var j = marchingPoints.GetLength(1) - 1;

            for (int i = 0; i < marchingPoints.GetLength(0) - 1; ++i)
            {
                for (int k = 0; k < marchingPoints.GetLength(2) - 1; ++k)
                {
                    var currentStartPoint = marchingPoints[i, j, k];
                    MarchingPoint currentEndPoint;

                    currentEndPoint = marchingPoints[i + 0, j + 0, k + 1];
                    edgeDictionary.Add(new MarchingPointPair(currentStartPoint, currentEndPoint), new MarchingEdge(currentStartPoint, currentEndPoint));

                    currentEndPoint = marchingPoints[i + 1, j + 0, k + 0];
                    edgeDictionary.Add(new MarchingPointPair(currentStartPoint, currentEndPoint), new MarchingEdge(currentStartPoint, currentEndPoint));
                }
            }
        }

        private static void AddFaceBoundryZEdgesToDictionary(MarchingPoint[, ,] marchingPoints, Dictionary<MarchingPointPair, MarchingEdge> edgeDictionary)
        {
            var k = marchingPoints.GetLength(2) - 1;

            for (int i = 0; i < marchingPoints.GetLength(0) - 1; ++i)
            {
                for (int j = 0; j < marchingPoints.GetLength(1) - 1; ++j)
                {
                    var currentStartPoint = marchingPoints[i, j, k];
                    MarchingPoint currentEndPoint;

                    currentEndPoint = marchingPoints[i + 0, j + 1, k + 0];
                    edgeDictionary.Add(new MarchingPointPair(currentStartPoint, currentEndPoint), new MarchingEdge(currentStartPoint, currentEndPoint));

                    currentEndPoint = marchingPoints[i + 1, j + 0, k + 0];
                    edgeDictionary.Add(new MarchingPointPair(currentStartPoint, currentEndPoint), new MarchingEdge(currentStartPoint, currentEndPoint));
                }
            }
        }

        private static void AddEdgeBoundryXEdgesToDictionary(MarchingPoint[, ,] marchingPoints, Dictionary<MarchingPointPair, MarchingEdge> edgeDictionary)
        {
            var j = marchingPoints.GetLength(1) - 1;
            var k = marchingPoints.GetLength(2) - 1;

            for (int i = 0; i < marchingPoints.GetLength(0) - 1; ++i)
            {
                var currentStartPoint = marchingPoints[i, j, k];
                MarchingPoint currentEndPoint;

                currentEndPoint = marchingPoints[i + 1, j + 0, k + 0];
                edgeDictionary.Add(new MarchingPointPair(currentStartPoint, currentEndPoint), new MarchingEdge(currentStartPoint, currentEndPoint));
            }
        }

        private static void AddEdgeBoundryYEdgesToDictionary(MarchingPoint[, ,] marchingPoints, Dictionary<MarchingPointPair, MarchingEdge> edgeDictionary)
        {
            var i = marchingPoints.GetLength(0) - 1;
            var k = marchingPoints.GetLength(2) - 1;

            for (int j = 0; j < marchingPoints.GetLength(1) - 1; ++j)
            {
                var currentStartPoint = marchingPoints[i, j, k];
                MarchingPoint currentEndPoint;

                currentEndPoint = marchingPoints[i + 0, j + 1, k + 0];
                edgeDictionary.Add(new MarchingPointPair(currentStartPoint, currentEndPoint), new MarchingEdge(currentStartPoint, currentEndPoint));
            }
        }

        private static void AddEdgeBoundryZEdgesToDictionary(MarchingPoint[, ,] marchingPoints, Dictionary<MarchingPointPair, MarchingEdge> edgeDictionary)
        {
            var i = marchingPoints.GetLength(0) - 1;
            var j = marchingPoints.GetLength(1) - 1;

            for (int k = 0; k < marchingPoints.GetLength(2) - 1; ++k)
            {
                var currentStartPoint = marchingPoints[i, j, k];
                MarchingPoint currentEndPoint;

                currentEndPoint = marchingPoints[i + 0, j + 0, k + 1];
                edgeDictionary.Add(new MarchingPointPair(currentStartPoint, currentEndPoint), new MarchingEdge(currentStartPoint, currentEndPoint));
            }
        }

        private static MarchingCube[, ,] CreateCubes(MarchingPoint[, ,] marchingPoints, Dictionary<MarchingPointPair, MarchingEdge> marchingEdgeDictionary)
        {
            var result = new MarchingCube[marchingPoints.GetLength(0) - 1, marchingPoints.GetLength(1) - 1, marchingPoints.GetLength(2) - 1];

            for (int i = 0; i < result.GetLength(0); ++i)
            {
                for (int j = 0; j < result.GetLength(1); ++j)
                {
                    for (int k = 0; k < result.GetLength(2); ++k)
                    {
                        var nXnYnZPoint = marchingPoints[i + 0, j + 0, k + 0];
                        var nXnYpZPoint = marchingPoints[i + 0, j + 0, k + 1];
                        var nXpYnZPoint = marchingPoints[i + 0, j + 1, k + 0];
                        var nXpYpZPoint = marchingPoints[i + 0, j + 1, k + 1];
                        var pXnYnZPoint = marchingPoints[i + 1, j + 0, k + 0];
                        var pXnYpZPoint = marchingPoints[i + 1, j + 0, k + 1];
                        var pXpYnZPoint = marchingPoints[i + 1, j + 1, k + 0];
                        var pXpYpZPoint = marchingPoints[i + 1, j + 1, k + 1];

                        var nXnYzZEdge = marchingEdgeDictionary[
                            new MarchingPointPair(
                                nXnYnZPoint,
                                nXnYpZPoint
                            )
                        ];
                        var nXpYzZEdge = marchingEdgeDictionary[
                            new MarchingPointPair(
                                nXpYnZPoint,
                                nXpYpZPoint
                            )
                        ];
                        var pXnYzZEdge = marchingEdgeDictionary[
                            new MarchingPointPair(
                                pXnYnZPoint,
                                pXnYpZPoint
                            )
                        ];
                        var pXpYzZEdge = marchingEdgeDictionary[
                            new MarchingPointPair(
                                pXpYnZPoint,
                                pXpYpZPoint
                            )
                        ];

                        var nXzYnZEdge = marchingEdgeDictionary[
                            new MarchingPointPair(
                                nXnYnZPoint,
                                nXpYnZPoint
                            )
                        ];
                        var pXzYnZEdge = marchingEdgeDictionary[
                            new MarchingPointPair(
                                pXnYnZPoint,
                                pXpYnZPoint
                            )
                        ];
                        var nXzYpZEdge = marchingEdgeDictionary[
                            new MarchingPointPair(
                                nXnYpZPoint,
                                nXpYpZPoint
                            )
                        ];
                        var pXzYpZEdge = marchingEdgeDictionary[
                            new MarchingPointPair(
                                pXnYpZPoint,
                                pXpYpZPoint
                            )
                        ];

                        var zXnYnZEdge = marchingEdgeDictionary[
                            new MarchingPointPair(
                                nXnYnZPoint,
                                pXnYnZPoint
                            )
                        ];
                        var zXnYpZEdge = marchingEdgeDictionary[
                            new MarchingPointPair(
                                nXnYpZPoint,
                                pXnYpZPoint
                            )
                        ];
                        var zXpYnZEdge = marchingEdgeDictionary[
                            new MarchingPointPair(
                                nXpYnZPoint,
                                pXpYnZPoint
                            )
                        ];
                        var zXpYpZEdge = marchingEdgeDictionary[
                            new MarchingPointPair(
                                nXpYpZPoint,
                                pXpYpZPoint
                            )
                        ];
                    }
                }
            }

            return result;
        }
    }
}
