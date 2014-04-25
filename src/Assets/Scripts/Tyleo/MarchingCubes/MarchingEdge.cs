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
        private Vector3 _localSpaceCoordinates = Vector3.zero;
        private Vector3 _worldSpaceCoordinates = Vector3.zero;
        private Vector3 _edgeNormal = Vector3.zero;
        private int _edgeIndex = 0;

        public MarchingPoint MarchingPoint0 { get { return _marchingPoint0; } }
        public MarchingPoint MarchingPoint1 { get { return _marchingPoint1; } }

        public uint LastFrameTouched { get { return _lastFrameTouched; } }
        public Vector3 LocalSpaceCoordinates { get { return _localSpaceCoordinates; } }
        public Vector3 EdgeNormal { get { return _edgeNormal; } }
        public int VertexIndex { get { return _edgeIndex; } }

        public void ProcessEdge(uint lastFrameTouched, IEnumerable<MarchingEntity> marchingEntities, float intensityThreshold, MeshDataProvider meshData)
        {
            _lastFrameTouched = lastFrameTouched;
            _edgeIndex = meshData.GetCurrentEdgeIndex();
            SetLocalAndWorldSpaceCoordinates(intensityThreshold);
            _edgeNormal = GetEdgeNormal(marchingEntities);

            meshData.AddVertexAndNormal(_localSpaceCoordinates, _edgeNormal);
        }

        private void SetLocalAndWorldSpaceCoordinates(float intensityThreshold)
        {
            var interpolant = (intensityThreshold - _marchingPoint0.Intensity) / (_marchingPoint1.Intensity - _marchingPoint0.Intensity);

            _localSpaceCoordinates =
                Vector3.Lerp(
                    _marchingPoint0.LocalSpaceCoordinates,
                    _marchingPoint1.LocalSpaceCoordinates,
                    interpolant
                );

            _worldSpaceCoordinates =
                Vector3.Lerp(
                    _marchingPoint0.WorldSpaceCoordinates,
                    _marchingPoint1.WorldSpaceCoordinates,
                    interpolant
                );
        }

        private Vector3 GetEdgeNormal(IEnumerable<MarchingEntity> marchingEntities)
        {
            var normal = Vector3.zero;
            foreach (var marchingEntity in marchingEntities)
            {
                var edgeToEntity = marchingEntity.transform.position - _worldSpaceCoordinates;
                normal += edgeToEntity * marchingEntity.GetIntensity(_worldSpaceCoordinates);
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
