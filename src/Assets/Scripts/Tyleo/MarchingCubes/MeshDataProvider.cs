using System.Collections.Generic;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MeshDataProvider
    {
        private readonly Vector3[] _vertices;
        private readonly int[] _triangles;
        private readonly Vector3[] _normals;

        private int _currentEdgeIndex = 0;
        private int _currentTriangleVertexIndexIndex = 0;

        public int GetCurrentEdgeIndex()
        {
            return _currentEdgeIndex;
        }

        public Vector3[] GetVetrices()
        {
            return _vertices;
        }

        public int[] GetTriangles()
        {
            return _triangles;
        }

        public Vector3[] GetNormals()
        {
            return _normals;
        }

        public void AddVertexAndNormal(Vector3 vertex, Vector3 normal)
        {
            _vertices[_currentEdgeIndex] = vertex;
            _normals[_currentEdgeIndex] = normal;
            _currentEdgeIndex++;
        }

        public void AddTriangleVertexIndex(int triangleVertexIndex)
        {
            _triangles[_currentTriangleVertexIndexIndex++] = triangleVertexIndex;
        }
        public MeshDataProvider(int numberOfVertices, int numberOfTriangleIndices)
        {
            _vertices = new Vector3[numberOfVertices];
            _normals = new Vector3[numberOfVertices];
            _triangles = new int[numberOfTriangleIndices];
        }
    }
}
