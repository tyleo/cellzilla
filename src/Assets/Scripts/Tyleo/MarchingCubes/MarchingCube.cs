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

        private readonly MarchingEdge _zXnYnZEdge;
        private readonly MarchingEdge _zXnYpZEdge;
        private readonly MarchingEdge _zXpYnZEdge;
        private readonly MarchingEdge _zXpYpZEdge;
        private readonly MarchingEdge _nXzYnZEdge;
        private readonly MarchingEdge _pXzYnZEdge;
        private readonly MarchingEdge _nXzYpZEdge;
        private readonly MarchingEdge _pXzYpZEdge;
        private readonly MarchingEdge _nXnYzZEdge;
        private readonly MarchingEdge _nXpYzZEdge;
        private readonly MarchingEdge _pXnYzZEdge;
        private readonly MarchingEdge _pXpYzZEdge;

        private uint _lastFrameTouched = 0;

        public MarchingPoint NxNyNzPoint { get { return _nXnYnZPoint; } }
        public MarchingPoint NxNyPzPoint { get { return _nXnYpZPoint; } }
        public MarchingPoint NxPyNzPoint { get { return _nXpYnZPoint; } }
        public MarchingPoint NxPyPzPoint { get { return _nXpYpZPoint; } }
        public MarchingPoint PxNyNzPoint { get { return _pXnYnZPoint; } }
        public MarchingPoint PxNyPzPoint { get { return _pXnYpZPoint; } }
        public MarchingPoint PxPyNzPoint { get { return _pXpYnZPoint; } }
        public MarchingPoint PxPyPzPoint { get { return _pXpYpZPoint; } }

        public MarchingEdge ZxNyNzEdge { get { return _zXnYnZEdge; } }
        public MarchingEdge ZxNyPzEdge { get { return _zXnYpZEdge; } }
        public MarchingEdge ZxPyNzEdge { get { return _zXpYnZEdge; } }
        public MarchingEdge ZxPyPzEdge { get { return _zXpYpZEdge; } }
        public MarchingEdge NxZyNzEdge { get { return _nXzYnZEdge; } }
        public MarchingEdge PxZyNzEdge { get { return _pXzYnZEdge; } }
        public MarchingEdge NxZyPzEdge { get { return _nXzYpZEdge; } }
        public MarchingEdge PxZyPzEdge { get { return _pXzYpZEdge; } }
        public MarchingEdge NxNyZzEdge { get { return _nXnYzZEdge; } }
        public MarchingEdge NxPyZzEdge { get { return _nXpYzZEdge; } }
        public MarchingEdge PxNyZzEdge { get { return _pXnYzZEdge; } }
        public MarchingEdge PxPyZzEdge { get { return _pXpYzZEdge; } }

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
                    meshData.AddTriangleVertexIndex(GetIntegralEdgeIndex(edgeIndex));
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

        private int GetIntegralEdgeIndex(EdgeIndex edgeIndex)
        {
            switch (edgeIndex)
            {
                case EdgeIndex.None:
                    throw new Exception();
                case EdgeIndex.ZxNyNz:
                    return ZxNyNzEdge.EdgeIndex;
                case EdgeIndex.ZxNyPz:
                    return ZxNyPzEdge.EdgeIndex;
                case EdgeIndex.ZxPyNz:
                    return ZxPyNzEdge.EdgeIndex;
                case EdgeIndex.ZxPyPz:
                    return ZxPyPzEdge.EdgeIndex;
                case EdgeIndex.NxZyNz:
                    return NxZyNzEdge.EdgeIndex;
                case EdgeIndex.PxZyNz:
                    return PxZyNzEdge.EdgeIndex;
                case EdgeIndex.NxZyPz:
                    return NxZyPzEdge.EdgeIndex;
                case EdgeIndex.PxZyPz:
                    return PxZyPzEdge.EdgeIndex;
                case EdgeIndex.NxNyZz:
                    return NxNyZzEdge.EdgeIndex;
                case EdgeIndex.NxPyZz:
                    return NxPyZzEdge.EdgeIndex;
                case EdgeIndex.PxNyZz:
                    return PxNyZzEdge.EdgeIndex;
                case EdgeIndex.PxPyZz:
                    return PxPyZzEdge.EdgeIndex;
                default:
                    throw new Exception;
            }
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

            _zXnYnZEdge = zXnYnZEdge;
            _zXnYpZEdge = zXnYpZEdge;
            _zXpYnZEdge = zXpYnZEdge;
            _zXpYpZEdge = zXpYpZEdge;
            _nXzYnZEdge = nXzYnZEdge;
            _pXzYnZEdge = pXzYnZEdge;
            _nXzYpZEdge = nXzYpZEdge;
            _pXzYpZEdge = pXzYpZEdge;
            _nXnYzZEdge = nXnYzZEdge;
            _nXpYzZEdge = nXpYzZEdge;
            _pXnYzZEdge = pXnYzZEdge;
            _pXpYzZEdge = pXpYzZEdge;
        }
    }
}
