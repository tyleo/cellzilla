using System.Collections.Generic;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MeshBuilder
    {
        private readonly Queue<Vector3> _vertices;
        private readonly Queue<Vector2> _uv;
        private readonly Queue<int> _triangles;
        private readonly Queue<Vector3> _normals;

        public Vector3[] GetVetrices()
        {
            return _vertices.ToArray();
        }

        public Vector2[] GetUV()
        {
            return _uv.ToArray();
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

        public void AddUV(Vector2 uv)
        {
            _uv.Enqueue(uv);
        }

        public void AddTriangle(int triangleIndex0, int triangleIndex1, int triangleIndex2)
        {
            _triangles.Enqueue(triangleIndex0);
            _triangles.Enqueue(triangleIndex1);
            _triangles.Enqueue(triangleIndex2);
        }

        public void AddNormal(Vector3 normal)
        {
            _normals.Enqueue(normal);
        }

        public MeshBuilder(int predictedNumberOfVertices)
        {
            _vertices = new Queue<Vector3>(predictedNumberOfVertices);
            _uv = new Queue<Vector2>(predictedNumberOfVertices);
            _triangles = new Queue<int>(predictedNumberOfVertices * 3);
            _normals = new Queue<Vector3>(predictedNumberOfVertices);
        }
    }
}
