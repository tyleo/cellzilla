using System.Diagnostics;

namespace Tyleo.MarchingCubes
{
    public sealed class MarchingCube
    {
        private readonly MarchingPoint _pXpYpZPoint;
        private readonly MarchingPoint _nXpYpZPoint;
        private readonly MarchingPoint _pXnYpZPoint;
        private readonly MarchingPoint _nXnYpZPoint;
        private readonly MarchingPoint _pXpYnZPoint;
        private readonly MarchingPoint _nXpYnZPoint;
        private readonly MarchingPoint _pXnYnZPoint;
        private readonly MarchingPoint _nXnYnZPoint;

        private readonly MarchingEdge _pXpYzZEdge;
        private readonly MarchingEdge _nXpYzZEdge;
        private readonly MarchingEdge _pXnYzZEdge;
        private readonly MarchingEdge _nXnYzZEdge;
        private readonly MarchingEdge _pXzYpZEdge;
        private readonly MarchingEdge _nXzYpZEdge;
        private readonly MarchingEdge _pXzYnZEdge;
        private readonly MarchingEdge _nXzYnZEdge;
        private readonly MarchingEdge _zXpYpZEdge;
        private readonly MarchingEdge _zXnYpZEdge;
        private readonly MarchingEdge _zXpYnZEdge;
        private readonly MarchingEdge _zXnYnZEdge;

        public MarchingCube(
            MarchingPoint pXpYpZPoint,
            MarchingPoint nXpYpZPoint,
            MarchingPoint pXnYpZPoint,
            MarchingPoint nXnYpZPoint,
            MarchingPoint pXpYnZPoint,
            MarchingPoint nXpYnZPoint,
            MarchingPoint pXnYnZPoint,
            MarchingPoint nXnYnZPoint,

            MarchingEdge pXpYzZEdge,
            MarchingEdge nXpYzZEdge,
            MarchingEdge pXnYzZEdge,
            MarchingEdge nXnYzZEdge,
            MarchingEdge pXzYpZEdge,
            MarchingEdge nXzYpZEdge,
            MarchingEdge pXzYnZEdge,
            MarchingEdge nXzYnZEdge,
            MarchingEdge zXpYpZEdge,
            MarchingEdge zXnYpZEdge,
            MarchingEdge zXpYnZEdge,
            MarchingEdge zXnYnZEdge
        )
        {
            Debug.Assert(
                pXpYzZEdge.MarchingPoint0.Equals(pXpYnZPoint) &&
                pXpYzZEdge.MarchingPoint1.Equals(pXpYpZPoint)
            );

            Debug.Assert(
                nXpYzZEdge.MarchingPoint0.Equals(nXpYnZPoint) &&
                nXpYzZEdge.MarchingPoint1.Equals(nXpYpZPoint)
            );

            Debug.Assert(
                pXnYzZEdge.MarchingPoint0.Equals(pXnYnZPoint) &&
                pXnYzZEdge.MarchingPoint1.Equals(pXnYpZPoint)
            );

            Debug.Assert(
                nXnYzZEdge.MarchingPoint0.Equals(nXnYnZPoint) &&
                nXnYzZEdge.MarchingPoint1.Equals(nXnYpZPoint)
            );

            Debug.Assert(
                pXzYpZEdge.MarchingPoint0.Equals(pXnYpZPoint) &&
                pXzYpZEdge.MarchingPoint1.Equals(pXpYpZPoint)
            );

            Debug.Assert(
                nXzYpZEdge.MarchingPoint0.Equals(nXnYpZPoint) &&
                nXzYpZEdge.MarchingPoint1.Equals(nXpYpZPoint)
            );

            Debug.Assert(
                pXzYnZEdge.MarchingPoint0.Equals(pXnYnZPoint) &&
                pXzYnZEdge.MarchingPoint1.Equals(pXpYnZPoint)
            );

            Debug.Assert(
                nXzYnZEdge.MarchingPoint0.Equals(nXnYnZPoint) &&
                nXzYnZEdge.MarchingPoint1.Equals(nXpYnZPoint)
            );

            Debug.Assert(
                zXpYpZEdge.MarchingPoint0.Equals(nXpYpZPoint) &&
                zXpYpZEdge.MarchingPoint1.Equals(pXpYpZPoint)
            );

            Debug.Assert(
                zXnYpZEdge.MarchingPoint0.Equals(nXnYpZPoint) &&
                zXnYpZEdge.MarchingPoint1.Equals(pXnYpZPoint)
            );

            Debug.Assert(
                zXpYnZEdge.MarchingPoint0.Equals(nXpYnZPoint) &&
                zXpYnZEdge.MarchingPoint1.Equals(pXpYnZPoint)
            );

            Debug.Assert(
                zXnYnZEdge.MarchingPoint0.Equals(nXnYnZPoint) &&
                zXnYnZEdge.MarchingPoint1.Equals(pXnYnZPoint)
            );

            _pXpYpZPoint = pXpYpZPoint;
            _nXpYpZPoint = nXpYpZPoint;
            _pXnYpZPoint = pXnYpZPoint;
            _nXnYpZPoint = nXnYpZPoint;
            _pXpYnZPoint = pXpYnZPoint;
            _nXpYnZPoint = nXpYnZPoint;
            _pXnYnZPoint = pXnYnZPoint;
            _nXnYnZPoint = nXnYnZPoint;

            _pXpYzZEdge = pXpYzZEdge;
            _nXpYzZEdge = nXpYzZEdge;
            _pXnYzZEdge = pXnYzZEdge;
            _nXnYzZEdge = nXnYzZEdge;
            _pXzYpZEdge = pXzYpZEdge;
            _nXzYpZEdge = nXzYpZEdge;
            _pXzYnZEdge = pXzYnZEdge;
            _nXzYnZEdge = nXzYnZEdge;
            _zXpYpZEdge = zXpYpZEdge;
            _zXnYpZEdge = zXnYpZEdge;
            _zXpYnZEdge = zXpYnZEdge;
            _zXnYnZEdge = zXnYnZEdge;
        }
    }
}
