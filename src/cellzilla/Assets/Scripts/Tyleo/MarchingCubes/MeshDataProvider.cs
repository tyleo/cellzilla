using UnityEngine;

namespace Tyleo.MarchingCubes
{
    /// <summary>
    /// Consumes data which allows a mesh to be easily updated and provides a method for updating
    /// meshes with the data.
    /// </summary>
    public sealed class MeshDataProvider
    {
        private readonly Vector3[] _vertices;
        private readonly int[] _triangles;
        private readonly Vector3[] _normals;

        private int _currentEdgeIndex = 0;
        private int _currentTriangleVertexIndexIndex = 0;


        public int CurrentEdgeIndex { get { return _currentEdgeIndex; } }

        /// <summary>
        /// Adds a vertex and normal to the mesh data and updates the CurrentEdgeIndex.
        /// </summary>
        /// <param name="vertex">
        /// The vertex to add.
        /// </param>
        /// <param name="normal">
        /// The normal of the vertex.
        /// </param>
        public void AddVertexAndNormal(Vector3 vertex, Vector3 normal)
        {
            _vertices[_currentEdgeIndex] = vertex;
            _normals[_currentEdgeIndex] = normal;
            _currentEdgeIndex++;
        }

        /// <summary>
        /// Adds a triangle index to the mesh data.
        /// </summary>
        /// <param name="triangleVertexIndex">
        /// The triangle index to add to the mesh data.
        /// </param>
        public void AddTriangleVertexIndex(int triangleVertexIndex)
        {
            _triangles[_currentTriangleVertexIndexIndex++] = triangleVertexIndex;
        }

        /// <summary>
        /// Updates a mesh with the current mesh data.
        /// </summary>
        /// <param name="mesh">
        /// The mesh to update.
        /// </param>
        /// <param name="uvCreator">
        /// A class which can be used to create UVs for the mesh.
        /// </param>
        public void UpdateMesh(Mesh mesh, UVCreator uvCreator = null)
        {
            mesh.vertices = _vertices;
            mesh.uv = uvCreator == null ? new Vector2[mesh.vertices.Length] : uvCreator.CreateUVs(mesh.vertices);
            mesh.triangles = _triangles;
            mesh.normals = _normals;
        }

        /// <summary>
        /// Creates a new MeshDataProvider.
        /// </summary>
        /// <param name="numberOfVertices">
        /// The maximum number of vertices the mesh can have.
        /// </param>
        /// <param name="numberOfTriangleIndices">
        /// The maximum number of triangle indices the mesh can have.
        /// </param>
        public MeshDataProvider(int numberOfVertices, int numberOfTriangleIndices)
        {
            _vertices = new Vector3[numberOfVertices];
            _normals = new Vector3[numberOfVertices];
            _triangles = new int[numberOfTriangleIndices];
        }
    }
}
