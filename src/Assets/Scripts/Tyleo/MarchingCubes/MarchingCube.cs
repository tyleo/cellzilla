using System;
using System.Collections.Generic;
using UnityEngine;
using MsDebug = System.Diagnostics.Debug;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingCube
    {
        private readonly MarchingPoint _nXnYnZPoint;
        private readonly MarchingPoint _nXnYpZPoint;
        private readonly MarchingPoint _nXpYnZPoint;
        private readonly MarchingPoint _nXpYpZPoint;
        private readonly MarchingPoint _pXnYnZPoint;
        private readonly MarchingPoint _pXnYpZPoint;
        private readonly MarchingPoint _pXpYnZPoint;
        private readonly MarchingPoint _pXpYpZPoint;

        private readonly MarchingEdge[] _machingEdges = new MarchingEdge[12];

        private uint _lastFrameTouched = 0;

        public MarchingPoint NxNyNzPoint { get { return _nXnYnZPoint; } }
        public MarchingPoint NxNyPzPoint { get { return _nXnYpZPoint; } }
        public MarchingPoint NxPyNzPoint { get { return _nXpYnZPoint; } }
        public MarchingPoint NxPyPzPoint { get { return _nXpYpZPoint; } }
        public MarchingPoint PxNyNzPoint { get { return _pXnYnZPoint; } }
        public MarchingPoint PxNyPzPoint { get { return _pXnYpZPoint; } }
        public MarchingPoint PxPyNzPoint { get { return _pXpYnZPoint; } }
        public MarchingPoint PxPyPzPoint { get { return _pXpYpZPoint; } }

        public MarchingEdge ZxNyNzEdge { get { return GetEdge(EdgeIndex.ZxNyNz); } set { SetEdge(EdgeIndex.ZxNyNz, value); } }
        public MarchingEdge ZxNyPzEdge { get { return GetEdge(EdgeIndex.ZxNyPz); } set { SetEdge(EdgeIndex.ZxNyPz, value); } }
        public MarchingEdge ZxPyNzEdge { get { return GetEdge(EdgeIndex.ZxPyNz); } set { SetEdge(EdgeIndex.ZxPyNz, value); } }
        public MarchingEdge ZxPyPzEdge { get { return GetEdge(EdgeIndex.ZxPyPz); } set { SetEdge(EdgeIndex.ZxPyPz, value); } }
        public MarchingEdge NxZyNzEdge { get { return GetEdge(EdgeIndex.NxZyNz); } set { SetEdge(EdgeIndex.NxZyNz, value); } }
        public MarchingEdge PxZyNzEdge { get { return GetEdge(EdgeIndex.PxZyNz); } set { SetEdge(EdgeIndex.PxZyNz, value); } }
        public MarchingEdge NxZyPzEdge { get { return GetEdge(EdgeIndex.NxZyPz); } set { SetEdge(EdgeIndex.NxZyPz, value); } }
        public MarchingEdge PxZyPzEdge { get { return GetEdge(EdgeIndex.PxZyPz); } set { SetEdge(EdgeIndex.PxZyPz, value); } }
        public MarchingEdge NxNyZzEdge { get { return GetEdge(EdgeIndex.NxNyZz); } set { SetEdge(EdgeIndex.NxNyZz, value); } }
        public MarchingEdge NxPyZzEdge { get { return GetEdge(EdgeIndex.NxPyZz); } set { SetEdge(EdgeIndex.NxPyZz, value); } }
        public MarchingEdge PxNyZzEdge { get { return GetEdge(EdgeIndex.PxNyZz); } set { SetEdge(EdgeIndex.PxNyZz, value); } }
        public MarchingEdge PxPyZzEdge { get { return GetEdge(EdgeIndex.PxPyZz); } set { SetEdge(EdgeIndex.PxPyZz, value); } }

        private MarchingEdge GetEdge(EdgeIndex edgeIndex)
        {
            return _machingEdges[(ushort)edgeIndex];
        }

        private void SetEdge(EdgeIndex edgeIndex, MarchingEdge value)
        {
            _machingEdges[(ushort)edgeIndex] = value;
        }

        public uint LastFrameTouched { get { return _lastFrameTouched; } }

        public bool Process(uint currentFrameIndex, Transform environmentTransform, IEnumerable<MarchingEntity> marchingEntities, float intensityThreshold, MeshDataProvider meshData)
        {
            _lastFrameTouched = currentFrameIndex;

            ProcessPoints(environmentTransform, marchingEntities);

            return ProcessEdges(marchingEntities, intensityThreshold, meshData);
        }

        private void ProcessPoints(Transform environmentTransform, IEnumerable<MarchingEntity> marchingEntities)
        {
            ProcessPoint(NxNyNzPoint, environmentTransform, marchingEntities);
            ProcessPoint(NxNyPzPoint, environmentTransform, marchingEntities);
            ProcessPoint(NxPyNzPoint, environmentTransform, marchingEntities);
            ProcessPoint(NxPyPzPoint, environmentTransform, marchingEntities);
            ProcessPoint(PxNyNzPoint, environmentTransform, marchingEntities);
            ProcessPoint(PxNyPzPoint, environmentTransform, marchingEntities);
            ProcessPoint(PxPyNzPoint, environmentTransform, marchingEntities);
            ProcessPoint(PxPyPzPoint, environmentTransform, marchingEntities);
        }

        private void ProcessPoint(MarchingPoint point, Transform environmentTransform, IEnumerable<MarchingEntity> marchingEntities)
        {
            if (point.LastFrameTouched != LastFrameTouched)
            {
                point.Process(LastFrameTouched, environmentTransform, marchingEntities);
            }
        }

        private bool ProcessEdges(IEnumerable<MarchingEntity> marchingEntities, float intensityThreshold, MeshDataProvider meshData)
        {
            var activatedPointFlags = GetPointFlags(intensityThreshold);

            var activatedEdgeFlags = PointFlagsToEdgeConverter.GetEdgeFlagsFromPointFlags(activatedPointFlags);

            if (activatedEdgeFlags != EdgeFlags.None)
            {
                if (activatedEdgeFlags.HasFlags(EdgeFlags.ZxNyNz))
                {
                    ProcessEdge(ZxNyNzEdge, marchingEntities, intensityThreshold, meshData);
                }
                if (activatedEdgeFlags.HasFlags(EdgeFlags.ZxNyPz))
                {
                    ProcessEdge(ZxNyPzEdge, marchingEntities, intensityThreshold, meshData);
                }
                if (activatedEdgeFlags.HasFlags(EdgeFlags.ZxPyNz))
                {
                    ProcessEdge(ZxPyNzEdge, marchingEntities, intensityThreshold, meshData);
                }
                if (activatedEdgeFlags.HasFlags(EdgeFlags.ZxPyPz))
                {
                    ProcessEdge(ZxPyPzEdge, marchingEntities, intensityThreshold, meshData);
                }
                if (activatedEdgeFlags.HasFlags(EdgeFlags.NxZyNz))
                {
                    ProcessEdge(NxZyNzEdge, marchingEntities, intensityThreshold, meshData);
                }
                if (activatedEdgeFlags.HasFlags(EdgeFlags.PxZyNz))
                {
                    ProcessEdge(PxZyNzEdge, marchingEntities, intensityThreshold, meshData);
                }
                if (activatedEdgeFlags.HasFlags(EdgeFlags.NxZyPz))
                {
                    ProcessEdge(NxZyPzEdge, marchingEntities, intensityThreshold, meshData);
                }
                if (activatedEdgeFlags.HasFlags(EdgeFlags.PxZyPz))
                {
                    ProcessEdge(PxZyPzEdge, marchingEntities, intensityThreshold, meshData);
                }
                if (activatedEdgeFlags.HasFlags(EdgeFlags.NxNyZz))
                {
                    ProcessEdge(NxNyZzEdge, marchingEntities, intensityThreshold, meshData);
                }
                if (activatedEdgeFlags.HasFlags(EdgeFlags.NxPyZz))
                {
                    ProcessEdge(NxPyZzEdge, marchingEntities, intensityThreshold, meshData);
                }
                if (activatedEdgeFlags.HasFlags(EdgeFlags.PxNyZz))
                {
                    ProcessEdge(PxNyZzEdge, marchingEntities, intensityThreshold, meshData);
                }
                if (activatedEdgeFlags.HasFlags(EdgeFlags.PxPyZz))
                {
                    ProcessEdge(PxPyZzEdge, marchingEntities, intensityThreshold, meshData);
                }

                foreach (var edgeIndex in PointFlagsToEdgeConverter.GetEdgeIndicesFromPointFlags(activatedPointFlags))
                {
                    meshData.AddTriangleVertexIndex(GetEdge(edgeIndex).VertexIndex);
                }

                return true;
            }

            return false;
        }

        private PointFlags GetPointFlags(float intensityThreshold)
        {
            return
                (NxNyNzPoint.Intensity > intensityThreshold ? PointFlags.NxNyNz : PointFlags.None) |
                (NxNyPzPoint.Intensity > intensityThreshold ? PointFlags.NxNyPz : PointFlags.None) |
                (NxPyNzPoint.Intensity > intensityThreshold ? PointFlags.NxPyNz : PointFlags.None) |
                (NxPyPzPoint.Intensity > intensityThreshold ? PointFlags.NxPyPz : PointFlags.None) |
                (PxNyNzPoint.Intensity > intensityThreshold ? PointFlags.PxNyNz : PointFlags.None) |
                (PxNyPzPoint.Intensity > intensityThreshold ? PointFlags.PxNyPz : PointFlags.None) |
                (PxPyNzPoint.Intensity > intensityThreshold ? PointFlags.PxPyNz : PointFlags.None) |
                (PxPyPzPoint.Intensity > intensityThreshold ? PointFlags.PxPyPz : PointFlags.None);
        }

        private void ProcessEdge(MarchingEdge edge, IEnumerable<MarchingEntity> marchingEntities, float intensityThreshold, MeshDataProvider meshData)
        {
            if (edge.LastFrameTouched == LastFrameTouched)
            {
                return;
            }

            edge.ProcessEdge(LastFrameTouched, marchingEntities, intensityThreshold, meshData);
        }

        public MarchingCube(
            MarchingPoint nXnYnZPoint,
            MarchingPoint nXnYpZPoint,
            MarchingPoint nXpYnZPoint,
            MarchingPoint nXpYpZPoint,
            MarchingPoint pXnYnZPoint,
            MarchingPoint pXnYpZPoint,
            MarchingPoint pXpYnZPoint,
            MarchingPoint pXpYpZPoint,

            MarchingEdge zXnYnZEdge,
            MarchingEdge zXnYpZEdge,
            MarchingEdge zXpYnZEdge,
            MarchingEdge zXpYpZEdge,
            MarchingEdge nXzYnZEdge,
            MarchingEdge pXzYnZEdge,
            MarchingEdge nXzYpZEdge,
            MarchingEdge pXzYpZEdge,
            MarchingEdge nXnYzZEdge,
            MarchingEdge nXpYzZEdge,
            MarchingEdge pXnYzZEdge,
            MarchingEdge pXpYzZEdge
        )
        {
            MsDebug.Assert(
                zXnYnZEdge.MarchingPoint0.Equals(nXnYnZPoint) &&
                zXnYnZEdge.MarchingPoint1.Equals(pXnYnZPoint)
            );

            MsDebug.Assert(
                zXnYpZEdge.MarchingPoint0.Equals(nXnYpZPoint) &&
                zXnYpZEdge.MarchingPoint1.Equals(pXnYpZPoint)
            );

            MsDebug.Assert(
                zXpYnZEdge.MarchingPoint0.Equals(nXpYnZPoint) &&
                zXpYnZEdge.MarchingPoint1.Equals(pXpYnZPoint)
            );

            MsDebug.Assert(
                zXpYpZEdge.MarchingPoint0.Equals(nXpYpZPoint) &&
                zXpYpZEdge.MarchingPoint1.Equals(pXpYpZPoint)
            );

            MsDebug.Assert(
                nXzYnZEdge.MarchingPoint0.Equals(nXnYnZPoint) &&
                nXzYnZEdge.MarchingPoint1.Equals(nXpYnZPoint)
            );

            MsDebug.Assert(
                pXzYnZEdge.MarchingPoint0.Equals(pXnYnZPoint) &&
                pXzYnZEdge.MarchingPoint1.Equals(pXpYnZPoint)
            );

            MsDebug.Assert(
                nXzYpZEdge.MarchingPoint0.Equals(nXnYpZPoint) &&
                nXzYpZEdge.MarchingPoint1.Equals(nXpYpZPoint)
            );

            MsDebug.Assert(
                pXzYpZEdge.MarchingPoint0.Equals(pXnYpZPoint) &&
                pXzYpZEdge.MarchingPoint1.Equals(pXpYpZPoint)
            );

            MsDebug.Assert(
                nXnYzZEdge.MarchingPoint0.Equals(nXnYnZPoint) &&
                nXnYzZEdge.MarchingPoint1.Equals(nXnYpZPoint)
            );

            MsDebug.Assert(
                nXpYzZEdge.MarchingPoint0.Equals(nXpYnZPoint) &&
                nXpYzZEdge.MarchingPoint1.Equals(nXpYpZPoint)
            );

            MsDebug.Assert(
                pXnYzZEdge.MarchingPoint0.Equals(pXnYnZPoint) &&
                pXnYzZEdge.MarchingPoint1.Equals(pXnYpZPoint)
            );

            MsDebug.Assert(
                pXpYzZEdge.MarchingPoint0.Equals(pXpYnZPoint) &&
                pXpYzZEdge.MarchingPoint1.Equals(pXpYpZPoint)
            );

            _nXnYnZPoint = nXnYnZPoint;
            _nXnYpZPoint = nXnYpZPoint;
            _nXpYnZPoint = nXpYnZPoint;
            _nXpYpZPoint = nXpYpZPoint;
            _pXnYnZPoint = pXnYnZPoint;
            _pXnYpZPoint = pXnYpZPoint;
            _pXpYnZPoint = pXpYnZPoint;
            _pXpYpZPoint = pXpYpZPoint;

            ZxNyNzEdge = zXnYnZEdge;
            ZxNyPzEdge = zXnYpZEdge;
            ZxPyNzEdge = zXpYnZEdge;
            ZxPyPzEdge = zXpYpZEdge;
            NxZyNzEdge = nXzYnZEdge;
            PxZyNzEdge = pXzYnZEdge;
            NxZyPzEdge = nXzYpZEdge;
            PxZyPzEdge = pXzYpZEdge;
            NxNyZzEdge = nXnYzZEdge;
            NxPyZzEdge = nXpYzZEdge;
            PxNyZzEdge = pXnYzZEdge;
            PxPyZzEdge = pXpYzZEdge;
        }
    }
}
