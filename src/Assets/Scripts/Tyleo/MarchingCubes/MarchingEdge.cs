using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingEdge
    {
        private readonly MarchingPoint _marchingPoint0;
        private readonly MarchingPoint _marchingPoint1;
        private uint _lastFrameTouched = 0;
        private Vector3 _edgeVertex = Vector3.zero;
        private Vector3 _edgeNormal = Vector3.zero;
        private int _edgeIndex = 0;

        public MarchingPoint MarchingPoint0 { get { return _marchingPoint0; } }
        public MarchingPoint MarchingPoint1 { get { return _marchingPoint1; } }

        public uint LastFrameTouched { get { return _lastFrameTouched; } }
        public Vector3 EdgeVertex { get { return _edgeVertex; } }
        public Vector3 EdgeNormal { get { return _edgeNormal; } }
        public int EdgeIndex { get { return _edgeIndex; } }

        public void ProcessEdge(uint lastFrameTouched, IEnumerable<MarchingEntity> marchingEntities, float intensityThreshold, MeshDataProvider meshData)
        {
            _lastFrameTouched = lastFrameTouched;
            _edgeIndex = meshData.GetCurrentEdgeIndex();
            _edgeVertex = GetEdgeVertex(intensityThreshold);
            _edgeNormal = GetEdgeNormal(marchingEntities);

            meshData.AddVertex(_edgeVertex);
            meshData.AddNormal(_edgeNormal);
        }

        private Vector3 GetEdgeVertex(float intensityThreshold)
        {
            return
                Vector3.Lerp(
                    _marchingPoint0.WorldSpaceCoordinates,
                    _marchingPoint1.WorldSpaceCoordinates,
                    (intensityThreshold - _marchingPoint0.Intensity) / (_marchingPoint1.Intensity - _marchingPoint0.Intensity)
                );
        }

        private Vector3 GetEdgeNormal(IEnumerable<MarchingEntity> marchingEntities)
        {
            var normal = Vector3.zero;
            foreach (var marchingEntity in marchingEntities)
            {
                var entityToVertex = _edgeVertex - marchingEntity.transform.position;
                normal += entityToVertex * marchingEntity.GetIntensity(_edgeVertex);
            }
            return normal.normalized;
        }

        public override string ToString()
        {
            return
                new StringBuilder()
                .Append('{')
                .Append(_marchingPoint0)
                .Append(", ")
                .Append(_marchingPoint1)
                .Append('}')
                .ToString();
        }

        public MarchingEdge(MarchingPoint marchingPoint0, MarchingPoint marchingPoint1)
        {
            _marchingPoint0 = marchingPoint0;
            _marchingPoint1 = marchingPoint1;
        }
    }
}
