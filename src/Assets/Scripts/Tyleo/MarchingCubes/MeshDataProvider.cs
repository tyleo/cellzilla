using System.Collections.Generic;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MeshDataProvider
    {
        private readonly Queue<Vector3> _vertices;
        private readonly Queue<int> _triangles;
        private readonly Queue<Vector3> _normals;

        public int GetCurrentEdgeIndex()
        {
            return _vertices.Count;
        }

        public Vector3[] GetVetrices()
        {
            return _vertices.ToArray();
        }

        public Vector2[] GetUV()
        {
            return new Vector2[0];
            throw new System.NotImplementedException();
        }

        public int[] GetTriangles()
        {
            return _triangles.ToArray();
        }

        public Vector3[] GetNormals()
        {
            return _normals.ToArray();
        }

        public void AddVertex(Vector3 vertex)
        {
            _vertices.Enqueue(vertex);
        }

        public void AddTriangleVertexIndex(int triangleVertexIndex)
        {
            _triangles.Enqueue(triangleVertexIndex);
        }

        public void AddNormal(Vector3 normal)
        {
            _normals.Enqueue(normal);
        }

        public MeshDataProvider(int predictedNumberOfVertices, int predictedNumberOfTriangleIndices)
        {
            _vertices = new Queue<Vector3>(predictedNumberOfVertices);
            _triangles = new Queue<int>(predictedNumberOfTriangleIndices);
            _normals = new Queue<Vector3>(predictedNumberOfVertices);
        }
    }
}
