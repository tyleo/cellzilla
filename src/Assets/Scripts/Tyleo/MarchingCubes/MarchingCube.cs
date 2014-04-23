using MsDebug = System.Diagnostics.Debug;
using System.Collections.Generic;
using UnityEngine;

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

        public bool Process(uint currentFrameIndex, Transform environmentTransform, IEnumerable<MarchingEntity> marchingEntities)
        {
            _lastFrameTouched = currentFrameIndex;

            ProcessPoints(environmentTransform, marchingEntities);

            ProcessEdges(marchingEntities);

            throw new System.NotImplementedException();
        }

        private void ProcessPoints(Transform environmentTransform, IEnumerable<MarchingEntity> marchingEntities)
        {
            if (NxNyNzPoint.LastFrameTouched != LastFrameTouched)
            {
                NxNyNzPoint.Process(LastFrameTouched, environmentTransform, marchingEntities);
            }
            if (NxNyPzPoint.LastFrameTouched != LastFrameTouched)
            {
                NxNyPzPoint.Process(LastFrameTouched, environmentTransform, marchingEntities);
            }
            if (NxPyNzPoint.LastFrameTouched != LastFrameTouched)
            {
                NxPyNzPoint.Process(LastFrameTouched, environmentTransform, marchingEntities);
            }
            if (NxPyPzPoint.LastFrameTouched != LastFrameTouched)
            {
                NxPyPzPoint.Process(LastFrameTouched, environmentTransform, marchingEntities);
            }
            if (PxNyNzPoint.LastFrameTouched != LastFrameTouched)
            {
                PxNyNzPoint.Process(LastFrameTouched, environmentTransform, marchingEntities);
            }
            if (PxNyPzPoint.LastFrameTouched != LastFrameTouched)
            {
                PxNyPzPoint.Process(LastFrameTouched, environmentTransform, marchingEntities);
            }
            if (PxPyNzPoint.LastFrameTouched != LastFrameTouched)
            {
                PxPyNzPoint.Process(LastFrameTouched, environmentTransform, marchingEntities);
            }
            if (PxPyPzPoint.LastFrameTouched != LastFrameTouched)
            {
                PxPyPzPoint.Process(LastFrameTouched, environmentTransform, marchingEntities);
            }
        }

        private void ProcessEdges(IEnumerable<MarchingEntity> marchingEntities)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateMesh(MeshDataProvider meshData)
        {
            throw new System.NotImplementedException();
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
